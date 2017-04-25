using System.Threading.Tasks;
using API.Data.Contracts;
using API.TransferModels.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ClassesController : Controller
    {
        private IClassRepository _repo;

        private readonly ClassesSirenHto _representation;

        public ClassesController(IClassRepository repo, ClassesSirenHto representation)
        {
            _repo = repo;
            _representation = representation;
        }

        [HttpGet("{id}", Name=Routes.ClassEntry)]
        public async Task<IActionResult> Get(int Id){
            return StatusCode(501, "Not Implemented");
        }
    }
}