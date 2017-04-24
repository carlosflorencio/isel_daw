using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api")]
    [AllowAnonymous]
    public class HomeController : Controller {

        public enum Actions
        {
            Index,
            Error
        }

        [HttpGet("/", Name = "Index")]
        public IActionResult Index() {
            return Ok("Home!");
        }


        [HttpGet("/error/{code}", Name = "Error")]
        public IActionResult Error(int code) {
            // Handle error here
            return StatusCode(code, "Status code " + code + ". This should be replaced with a nice media type!");
        }
    }
}
