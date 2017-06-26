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
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using static API.TransferModels.ResponseModels.HomeJsonHome;

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

        [HttpGet("/", Name = "Index")]
        public IActionResult Index()
        {
            var origin = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;
            var routes = _provider.ActionDescriptors.Items.Select(x =>
                new HomeEntity
                {
                    Name = x.AttributeRouteInfo.Name,
                    Url = origin + "/" + 
                        Regex.Replace(x.AttributeRouteInfo.Template, @":+[a-z]+", "")
                }).ToList();

            return Ok(HomeJsonHome.Home(routes));
        }


        [HttpGet("/error/{code}", Name = "Error")]
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
    }
}
