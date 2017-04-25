using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data.Contracts;
using API.Models;
using API.TransferModels.InputModels;
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

        public async Task<IActionResult> GetAll([FromQuery] ListQueryStringDto query)
        {
            List<Class> classes = await _repo.GetAllPaginatedAsync(query);

            var result = _representation.Collection(classes, query);

            return Ok(result);
        }

        [HttpGet("{id}", Name=Routes.ClassEntry)]
        public async Task<IActionResult> Get(int Id){

            Class c = await _repo.GetByIdAsync(Id);

            if(c == null){
                return NotFound();
            }

            return Ok(_representation.Entity(c));
        }

        [HttpPost("", Name=Routes.ClassCreate)]
        public async Task<IActionResult> Post([FromBody]ClassDTO dto)
        {
            return StatusCode(501, "Not Implemented");
        }

        [HttpPut("", Name=Routes.ClassEdit)]
        public async Task<IActionResult> Put([FromBody]ClassDTO dto)
        {
            return StatusCode(501, "Not Implemented");
        }

        [HttpDelete("{id}", Name=Routes.ClassDelete)]
        public async Task<IActionResult> Delete(int Id)
        {
            return StatusCode(501, "Not Implemented");
        }
    }
}