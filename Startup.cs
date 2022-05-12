using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Data.Repositories;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using StaffSuggestAPI.Entities;

namespace StaffSuggestAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // add cors
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options
                .SetIsOriginAllowed(_ => true)
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
                //.WithOrigins("http://localhost:52403", "http://localhost:4200"));
            });

            services.AddControllers(); 
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            /*services.AddDbContext<SuggestionContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("SuggestionContext"))
            );*/

            // -- configure the SQLConnectionString service..
            //services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionString"));

            // var connectionString = Configuration["ConnectionStrings:db"];

            services.AddDbContext<DataContext>(option => option.UseSqlServer(Configuration.GetConnectionString("db")));


            //services.AddScoped<IKycRepo, KycRepo>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Zenith Insurance Staff Suggestion API",
                    Description = "An ASP.NET Core Web API for Zenith Insurance EBusiness Personal Accident module application" +
                    "App extending the functionality of Ebusiness. The project was done in asp.net core 3.1",
                    TermsOfService = new Uri("http://www.zenithinsurance.com.ng/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Zenith Insurance IT Software Engineers",
                        Email = "ZGIDevelopers@zenithinsurance.com.ng",
                        Url = new Uri("http://www.zenithinsurance.com.ng/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Zenith Insurance License",
                        Url = new Uri("http://www.zenithinsurance.com.ng/"),
                    }
                });
            });


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IComplaintRepository, ComplaintRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                await next();
            });

            // app.UseMvc();

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });


            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./v1/swagger.json", "Personal Accident policy API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            // allow CORS
            app.UseCors("AllowOrigin");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
