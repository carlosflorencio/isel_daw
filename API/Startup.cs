using System;
using API.Data;
using API.Data.Contracts;
using API.Middlewares;
using API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Internal.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Startup
    {
        private IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // EF Core
            services.AddDbContext<DatabaseContext>(
                opt => opt.UseNpgsql(Configuration["Data:PostgreConnection:ConnectionString"])
            );

            // MVC
            services.AddMvc();

            // Custom Services
            // AddTransient -> services are created each time they are requested.
            // AddScoped    -> services are created once per request.
            // AddSingleton -> services are created only the first time.
            services.AddScoped<IStudentRepository, StudentRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

//            if (env.IsDevelopment()) {
//                app.UseDeveloperExceptionPage();
//            }

            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseMvcWithDefaultRoute();
        }
    }
}
