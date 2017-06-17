using System.Threading.Tasks;
using API.Data.Contracts;
using API.Models;
using API.Services;
using API.TransferModels.InputModels;
using API.TransferModels.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class CoursesController : Controller
    {
        private ICourseRepository _repo;

        private readonly CoursesSirenHto _representation;

        public CoursesController(ICourseRepository repo, CoursesSirenHto representation)
        {
            _repo = repo;
            _representation = representation;
        }

        [HttpGet("", Name=Routes.CourseList)]
        public async Task<IActionResult> GetAll([FromQuery] ListQueryStringDto query)
        {
            PagedList<Course> courses = await _repo.GetAllPaginatedAsync(query);

            return Ok(_representation.Collection(courses, query));
        }

        [HttpGet("{id}", Name=Routes.CourseEntry)]
        public async Task<IActionResult> Get(int Id)
        {

            Course course = await _repo.GetByIdAsync(Id);

            if(course == null){
                return NotFound();
            }

            return Ok(_representation.Entity(course));
        }
        
    }
}