// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Dynamic;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Logic.Home
{
    [SecurityHeaders]
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;

        public HomeController(IIdentityServerInteractionService interaction)
        {
            _interaction = interaction;
        }

        [HttpGet("/headers")]
        public IActionResult Headers()
        {
            dynamic obj = new ExpandoObject();
            obj.isHttps = HttpContext.Request.IsHttps;
            obj.protocol = HttpContext.Request.Protocol;
            obj.scheme = HttpContext.Request.Scheme;
            obj.headers = HttpContext.Request.Headers;

            return Ok(obj);
        }

        /// <summary>
        /// Shows the error page
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;
            }

            return View("Error", vm);
        }
    }
}