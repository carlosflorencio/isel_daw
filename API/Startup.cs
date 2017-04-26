using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
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
using API.Extensions;
using API.TransferModels.ResponseModels;
using FluentSiren.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

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
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));

                // Accept application/vnd.siren+json media type (json format)
                options.OutputFormatters.Add(new SirenOutputFormatter());
            }).AddJsonOptions(options =>
            {
                // Ignore null fields on json formats by default
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            // Custom Services
            // AddTransient -> services are created each time they are requested.
            // AddScoped    -> services are created once per request.
            // AddSingleton -> services are created only the first time.
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IClassRepository, ClassRepository>();

            // needed to inject IUrlHelper
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddScoped<StudentsSirenHto>();
            services.AddScoped<TeachersSirenHto>();
            services.AddScoped<ClassesSirenHto>();

        }

        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            DatabaseContext context,
            IStudentRepository studentRepo, ITeacherRepository teacherRepo)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug(LogLevel.Debug);

            app.UseMiddleware<BasicAuthMiddleware>();

            //app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseStatusCodePagesWithReExecute("/error/{0}");

            // Seed data if there is none
            if (env.IsDevelopment() && !context.Semesters.Any())
            {
                context.ClearAllData();
                context.EnsureSeedDataForContext();
            }

            app.UseMvcWithDefaultRoute();
        }
    }
}
