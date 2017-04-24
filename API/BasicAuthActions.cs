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

        public BasicAuthActions(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public Task AuthenticationFailed(AuthenticationFailedContext context) {
            context.Response.StatusCode = 400;

            return Task.FromResult(0);
        }

        public async Task ValidateCredentials(ValidateCredentialsContext context)
        {
            var std = await _studentRepository.GetByEmailAndPasswordAsync(context.Username, context.Password);
            if (std != null)
            {
                const string Issuer = "https://hdn.pt";

                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, std.Name, ClaimValueTypes.String, Issuer),
                    new Claim(ClaimTypes.Email, std.Email, ClaimValueTypes.String, Issuer),
                    new Claim(ClaimTypes.Role, Roles.Student, ClaimValueTypes.String, Issuer),
                };

                var identity = new ClaimsIdentity(claims, context.Options.AuthenticationScheme);

                context.Ticket = new AuthenticationTicket(new ClaimsPrincipal(identity),
                    new AuthenticationProperties(),
                    context.Options.AuthenticationScheme);
            }

            // Auth teachers here
        }

    }
}
