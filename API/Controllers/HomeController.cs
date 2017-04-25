using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.TransferModels.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api")]
    [AllowAnonymous]
    public class HomeController : Controller {

        [HttpGet("/", Name = "Index")]
        public IActionResult Index() {
            return Ok("Home!");
        }


        [HttpGet("/error/{code}", Name = "Error")]
        public IActionResult Error(int code) {

            var problem = new ProblemJson() {
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
