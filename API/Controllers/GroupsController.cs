using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class GroupsController : Controller
    {
        [HttpGet("{id}/students", Name=Routes.GroupStudentsList)]
        public async Task<IActionResult> GroupStudentsList(int Id)
        {
            return StatusCode(501, "Not Implemented");
        }

        [HttpGet("{id}", Name=Routes.GroupEntry)]
        public async Task<IActionResult> Get(int Id)
        {
            return StatusCode(501, "Not Implemented");
        }
    }
}