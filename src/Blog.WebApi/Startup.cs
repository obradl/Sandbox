using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using Blog.ApplicationCore.Behaviors;
using Blog.ApplicationCore.Features.Post.CreatePost;
using Blog.Infrastructure.Data;
using Blog.WebApi.Filters;
using Blog.WebApi.Middleware;
using FluentValidation.AspNetCore;
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
            services.AddOptions();
            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDbSettings"));

            services.AddScoped<IBlogContext, BlogContext>();
            services.AddMvc(options => { options.Filters.Add<ExceptionFilter>(); })
                .AddFluentValidation(fvc =>
                    fvc.RegisterValidatorsFromAssemblyContaining<CreatePostCommandValidator>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatePostExistence<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidateBehavior<,>));
            services.AddMediatR(typeof(CreatePostCommandHandler).GetTypeInfo().Assembly);

            services.AddAutoMapper();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Blog API", Version = "v1",
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
                        ContentTypes = {"application/problem+json", "application/problem+xml"}
                    };
                };
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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