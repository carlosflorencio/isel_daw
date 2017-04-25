using System.Threading.Tasks;
using API.Data.Contracts;
using API.TransferModels.InputModels;
using API.TransferModels.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class TeachersController : Controller
    {
        private ITeacherRepository _repo;

        private readonly TeachersSirenHto _representation;

        public TeachersController(ITeacherRepository repo, TeachersSirenHto representation)
        {
            _repo = repo;
            _representation = representation;
        }

        [HttpGet("", Name=Routes.TeacherList)]
        public async Task<IActionResult> GetAll([FromQuery]ListQueryStringDto query)
        {
            var teachers = await _repo.GetAllPaginatedAsync(query);

            var result = _representation.Collection(teachers, query);

            return Ok(result);
        }

        [HttpGet("{number}", Name=Routes.TeacherEntry)]
        public async Task<IActionResult> Get(int Number)
        {
            //TODO: Implement
            return StatusCode(501, "Not Implemented");
        }

        [HttpPost("", Name=Routes.TeacherCreate)]
        public async Task<IActionResult> Post([FromBody]string dto)
        {
            //TODO: Implement
            return StatusCode(501, "Not Implemented");
        }

        [HttpPut("{id}", Name=Routes.TeacherEdit)]
        public async Task<IActionResult> Put(int id, [FromBody]string value)
        {
            //TODO: Implement
            return StatusCode(501, "Not Implemented");
        }

        [HttpDelete("{id}", Name=Routes.TeacherDelete)]
        public async Task<IActionResult> Delete(int id)
        {
            //TODO: Implement
            return StatusCode(501, "Not Implemented");
        }

        [HttpGet("{number}/classes", Name = Routes.TeacherClassList)]
        public string TeacherClasses(int number, [FromQuery] ListQueryStringDto query)
        {
            return "value";
        }
    }
}