using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using API.TransferModels.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using static API.TransferModels.ResponseModels.HomeJsonHome;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace API.Controllers
{
    [Route("api")]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IActionDescriptorCollectionProvider _provider;

        public HomeController(IActionDescriptorCollectionProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("/", Name = Routes.Index)]
        public IActionResult Index()
        {
            var origin = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;

            // Convert all Get Actions from Controllers to a list of endpoints
            var routes = _provider.ActionDescriptors
                .Items
                .Where(actionIsValidRoute)
                .Select(r => parseRoute(r, origin))
                .ToList();

            return Ok(HomeJsonHome.Home(routes));
        }


        [HttpGet("/error/{code}", Name = Routes.Error)]
        public IActionResult Error(int code)
        {
            var problem = new ProblemJson()
            {
                Type = "type",
                Status = code,
                Title = "My title",
                Detail = "My details!"
            };

            // Handle error here
            return StatusCode(code, problem);
        }

        /*
        |-----------------------------------------------------------------------
        | Parse Routes
        |-----------------------------------------------------------------------
        */
        private bool actionIsValidRoute(ActionDescriptor ac)
        {
            return (ac as ControllerActionDescriptor)?.MethodInfo
                   .GetCustomAttributes<HttpGetAttribute>()
                   .FirstOrDefault() != default(HttpGetAttribute) &&
                   ac.AttributeRouteInfo.Name != null;
        }

        private HomeEntity parseRoute(ActionDescriptor x, string origin)
        {
            var path = x.AttributeRouteInfo.Template.ToLower();
            return new HomeEntity
            {
                Name = x.AttributeRouteInfo.Name,
                Url = origin + "/" + Regex.Replace(path, @":[a-z]+", "")
            };
        }
    }

}
