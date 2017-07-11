// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using API.Attribute;
using API.Data.Contracts;
using API.Models;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Account
{
    /// <summary>
    /// This sample controller implements a typical login/logout/provision workflow for local and external accounts.
    /// The login service encapsulates the interactions with the user data store. This data store is in-memory only and cannot be used for production!
    /// The interaction service provides a way for the UI to communicate with identityserver for validation and context retrieval
    /// </summary>
    [SecurityHeaders]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly AccountService _account;
        private readonly ITeacherRepository _teacherRepo;
        private readonly IStudentRepository _studentRepo;

        public AccountController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IHttpContextAccessor httpContextAccessor,
            IStudentRepository studentRepo,
            ITeacherRepository teacherRepo)
        {
            _studentRepo = studentRepo;
            _teacherRepo = teacherRepo;
            _interaction = interaction;
            _account = new AccountService(interaction, httpContextAccessor, clientStore);
        }

        /// <summary>
        /// Show login page
        /// </summary>
        [HttpGet("login")]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var vm = await _account.BuildLoginViewModelAsync(returnUrl);

            return View(vm);
        }

        /// <summary>
        /// Show logout page
        /// </summary>
        [HttpGet("logout")]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var vm = await _account.BuildLogoutViewModelAsync(logoutId);

            if (vm.ShowLogoutPrompt == false)
            {
                // no need to show prompt
                return await Logout(vm);
            }

            return View(vm);
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            if (ModelState.IsValid)
            {
                // validate username/password against in-memory store
                if(await AuthenticateUser(model.Email, model.Password, HttpContext))
                {
                    AuthenticationProperties props = null;
                    // only set explicit expiration here if persistent.
                    // otherwise we reply upon expiration configured in cookie middleware.
                    if (AccountOptions.AllowRememberLogin && model.RememberLogin)
                    {
                        props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                        };
                    };

                    // issue bearer authentication with User
                    await HttpContext.Authentication.SignInAsync(
                        HttpContext.User.Identity.Name,
                        HttpContext.User.Identity.Name,
                        props,
                        HttpContext.User.Claims.ToArray()
                    );

                    // make sure the returnUrl is still valid, and if yes 
                    //  redirect back to authorize endpoint
                    if (_interaction.IsValidReturnUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return Redirect("~/");
                }

                ModelState.AddModelError("", AccountOptions.InvalidCredentialsErrorMessage);
            }

            // something went wrong, show form with error
            var vm = await _account.BuildLoginViewModelAsync(model);
            return View(vm);
        }

        /// <summary>
        /// Handle logout page postback
        /// </summary>
        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutInputModel model)
        {
            var vm = await _account.BuildLoggedOutViewModelAsync(model.LogoutId);
            if (vm.TriggerExternalSignout)
            {
                string url = Url.Action("Logout", new { logoutId = vm.LogoutId });
                try
                {
                    // hack: try/catch to handle social providers that throw
                    await HttpContext.Authentication.SignOutAsync(vm.ExternalAuthenticationScheme,
                        new AuthenticationProperties { RedirectUri = url });
                }
                catch(NotSupportedException) // this is for the external providers that don't have signout
                {
                }
                catch(InvalidOperationException) // this is for Windows/Negotiate
                {
                }
            }

            // delete local authentication cookie
            await HttpContext.Authentication.SignOutAsync();

            return View("LoggedOut", vm);
        }

        private async Task<bool> AuthenticateUser(string username, string password, HttpContext context)
        {
            var std = await _studentRepo.GetByEmailAndPasswordAsync(username, password);
            if (std != null)
            {

                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, std.Name, ClaimValueTypes.String),
                    new Claim(ClaimTypes.Email, std.Email, ClaimValueTypes.String),
                    new Claim(ClaimTypes.Role, Roles.Student, ClaimValueTypes.String),
                };

                var identity = new ClaimsIdentity(claims, "Bearer");
                context.User = new ClaimsPrincipal(identity);

                return true;
            }

            var teacher = await _teacherRepo.GetByEmailAndPasswordAsync(username, password);
            if (teacher != null)
            {
                var claims = new List<Claim> {
                    new Claim(
                        ClaimTypes.Name, 
                        teacher.Name, 
                        ClaimValueTypes.String
                    ),
                    new Claim(
                        ClaimTypes.Email, 
                        teacher.Email, 
                        ClaimValueTypes.String
                    ),
                    new Claim(
                        ClaimTypes.Role, 
                        teacher.IsAdmin? Roles.Admin : Roles.Teacher, 
                        ClaimValueTypes.String
                    ),
                };

                var identity = new ClaimsIdentity(claims, "Bearer");
                context.User = new ClaimsPrincipal(identity);

                return true;
            }

            return false;
        }
    }
}