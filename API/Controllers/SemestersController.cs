using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class SemestersController : Controller
    {
        [HttpGet("", Name=Routes.SemesterEntry)]
        public async Task<IActionResult> Get(int Id)
        {
            return StatusCode(501, "Not Implemented");
        }
    }
}