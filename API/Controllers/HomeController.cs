using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api")]
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index() {
            return Ok("Home!");
        }


        [HttpGet("/error/{code}")]
        public IActionResult Error(int code)
        {
            // Handle error here
            return StatusCode(code, "Status code " + code + ". This should be replaced with a nice media type!");
        }
    }
}
