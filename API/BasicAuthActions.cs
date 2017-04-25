using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Data.Contracts;
using idunno.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Authentication;

namespace API
{
    public class BasicAuthActions : IBasicAuthenticationEvents
    {

        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;

        public BasicAuthActions(IStudentRepository studentRepository, ITeacherRepository teacherRepository) {
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
        }

        public Task AuthenticationFailed(AuthenticationFailedContext context) {
            context.Response.StatusCode = 400;

            return Task.FromResult(0);
        }

        // TODO: remove duplicate code
        public async Task ValidateCredentials(ValidateCredentialsContext context) {
            const string Issuer = "https://hdn.pt";

            var std = await _studentRepository.GetByEmailAndPasswordAsync(context.Username, context.Password);
            if (std != null) {

                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, std.Name, ClaimValueTypes.String, Issuer),
                    new Claim(ClaimTypes.Email, std.Email, ClaimValueTypes.String, Issuer),
                    new Claim(ClaimTypes.Role, Roles.Student, ClaimValueTypes.String, Issuer),
                };

                var identity = new ClaimsIdentity(claims, context.Options.AuthenticationScheme);

                context.Ticket = new AuthenticationTicket(
                    new ClaimsPrincipal(identity),
                    new AuthenticationProperties(),
                    context.Options.AuthenticationScheme);

                return;
            }

            var teacher = await _teacherRepository.GetByEmailAndPasswordAsync(context.Username, context.Password);
            if (teacher != null) {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, teacher.Name, ClaimValueTypes.String, Issuer),
                    new Claim(ClaimTypes.Email, teacher.Email, ClaimValueTypes.String, Issuer),
                    new Claim(ClaimTypes.Role, Roles.Teacher, ClaimValueTypes.String, Issuer),
                };

                if (teacher.IsAdmin) {
                    claims.Add(new Claim(ClaimTypes.Role, Roles.Admin, ClaimValueTypes.String, Issuer));
                }

                var identity = new ClaimsIdentity(claims, context.Options.AuthenticationScheme);

                context.Ticket = new AuthenticationTicket(
                    new ClaimsPrincipal(identity),
                    new AuthenticationProperties(),
                    context.Options.AuthenticationScheme);
                return;
            }
        }
    }
}
