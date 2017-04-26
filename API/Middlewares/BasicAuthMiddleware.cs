using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Data.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features.Authentication;

namespace API.Middlewares
{
    public class BasicAuthMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;

        public BasicAuthMiddleware(RequestDelegate next, IStudentRepository studentRepository, ITeacherRepository teacherRepository)
        {
            _next = next;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task Invoke(HttpContext context)
        {
            var existingPrincipal = context.Features.Get<IHttpAuthenticationFeature>()?.User;
            var handler = new BasicAuthHandler(context, existingPrincipal);
            AttachAuthenticationHandler(handler);
            try
            {
                string authHeader = context.Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Basic"))
                {
                    //Extract credentials
                    var token = authHeader.Substring("Basic ".Length).Trim();
                    var credentialstring = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                    var credentials = credentialstring.Split(':');

                    var username = credentials[0];
                    var password = credentials[1];

                    if (await IsValidUser(username, password, context))
                    {
                        await _next.Invoke(context);
                    }
                    else
                    {
                        InvalidLogin(context);
                    }
                }
                else
                {
                    InvalidLogin(context);
                }
            }
            finally
            {
                DetachAuthenticationhandler(handler);

                if (context.Response.ContentType == null &&
                    context.Response.StatusCode == 404 &&
                    context.User.Identity.IsAuthenticated) {
                    context.Response.StatusCode = 403;
                }
            }
        }

        private async Task<bool> IsValidUser(string username, string password, HttpContext context)
        {
            const string issuer = "https://hdn.pt";

            var std = await _studentRepository.GetByEmailAndPasswordAsync(username, password);
            if (std != null)
            {

                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, std.Name, ClaimValueTypes.String, issuer),
                    new Claim(ClaimTypes.Email, std.Email, ClaimValueTypes.String, issuer),
                    new Claim(ClaimTypes.Role, Roles.Student, ClaimValueTypes.String, issuer),
                };

                var identity = new ClaimsIdentity(claims, "Basic");
                context.User = new ClaimsPrincipal(identity);

                return true;
            }

            var teacher = await _teacherRepository.GetByEmailAndPasswordAsync(username, password);
            if (teacher != null)
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, teacher.Name, ClaimValueTypes.String, issuer),
                    new Claim(ClaimTypes.Email, teacher.Email, ClaimValueTypes.String, issuer),
                    new Claim(ClaimTypes.Role, Roles.Teacher, ClaimValueTypes.String, issuer),
                };

                if (teacher.IsAdmin)
                {
                    claims.Add(new Claim(ClaimTypes.Role, Roles.Admin, ClaimValueTypes.String, issuer));
                }

                var identity = new ClaimsIdentity(claims, "Basic");
                context.User = new ClaimsPrincipal(identity);

                return true;
            }

            return false;
        }

        private void InvalidLogin(HttpContext context)
        {
            context.Response.StatusCode = 401; //Unauthorized
            context.Response.Headers.Add("WWW-Authenticate", "Basic realm=\"You need auth.\"");
        }

        private void AttachAuthenticationHandler(BasicAuthHandler handler)
        {
            var auth = handler.HttpContext.Features.Get<IHttpAuthenticationFeature>();
            if (auth == null)
            {
                auth = new HttpAuthenticationFeature();
                handler.HttpContext.Features.Set(auth);
            }
            handler.PriorHandler = auth.Handler;
            auth.Handler = handler;
        }

        private void DetachAuthenticationhandler(BasicAuthHandler handler)
        {
            var auth = handler.HttpContext.Features.Get<IHttpAuthenticationFeature>();
            if (auth != null)
            {
                auth.Handler = handler.PriorHandler;
            }
        }
    }
}
