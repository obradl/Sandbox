using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using Blog.ApplicationCore.Behaviors;
using Blog.ApplicationCore.Features.Post.CreatePost;
using Blog.Infrastructure.ApiClients;
using Blog.Infrastructure.Data;
using Blog.WebApi.Filters;
using Blog.WebApi.HealthChecks;
using Blog.WebApi.Middleware;
using FluentValidation.AspNetCore;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Blog.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var configMongoDb = Configuration.GetSection("MongoDbSettings");
            services.AddOptions();
            services.Configure<MongoDbSettings>(configMongoDb);

            services.AddMvc(options => { options.Filters.Add<ExceptionFilter>(); })
                .AddFluentValidation(fvc =>
                    fvc.RegisterValidatorsFromAssemblyContaining<CreatePostCommandValidator>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IBlogContext, BlogContext>();
            services.AddHttpClient<GitHubService>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            //services.AddScoped(typeof(IPipelineBehavior<IPostRequest, object>), typeof(ValidatePostExistence<>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidateBehavior<,>));
            services.AddMediatR(typeof(CreatePostCommandHandler).GetTypeInfo().Assembly);

            services.AddHealthChecks()
                .AddCheck<MongoDbHealthCheck>()
                .AddCheck<GitHubHealthCheck>();

            //services.AddHangfire(configMongoDb["ConnectionString"], configMongoDb["Database"]);
            services.AddAutoMapper();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Blog API",
                        Version = "v1",
                        Description = "Health check endpoint: /health. Hangfire endpoint: /hangfire"
                    });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Please refer to the errors property for additional details."
                    };

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };
                };
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseHangfireDashboard();
            //app.UseHangfireServer();
            app.UseHealthCheck("/health");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog API");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.DevExceptionHandlerMiddleware();
            }
            else
            {
                app.UseExceptionHandlerMiddleware();
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
