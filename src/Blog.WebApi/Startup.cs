using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using Blog.ApplicationCore.Common.PostUtils;
using Blog.ApplicationCore.Features.Post.Commands.CreatePost;
using Blog.Infrastructure.Data;
using Blog.WebApi.Filters;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatePostExistsPipelineBehavior<,>));
            
            services.AddMvc(options =>
            {
                options.Filters.Add<ExceptionFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAutoMapper();
            services.AddMediatR(typeof(CreatePostCommandHandler).GetTypeInfo().Assembly);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", 
                    new Info {
                        Title = "Blog API", Version = "v1",
                        Description = "An API made with .Net core 2.1," +
                                      " Swashbuckle/Swagger, MediatR, MongoDb," +
                                      " FluentValidation, XUnit, Moq, AutoMapper, DDD-ish "
                    });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
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

            if (!env.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
