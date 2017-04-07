using System.Collections.Generic;
using System.Threading.Tasks;
using _1617_2_LI41N_G9.Data.Repositories;
using _1617_2_LI41N_G9.Models;
using _1617_2_LI41N_G9.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace _1617_2_LI41N_G9.Controllers
{
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private IRepository<Student> _repo;

        public StudentsController(IRepository<Student> repo)
        {
            _repo = repo;
        }

        // GET api/students
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return _repo.GetAll();
        }

        // GET api/students/5
        [HttpGet("{id}", Name="GetStudent")]
        public IActionResult Get(int id)
        {
            var entity = _repo.Find(id);
            if(entity == null){
                return NotFound();
            }
            return new ObjectResult(entity);
        }

        // POST api/students
        [HttpPost]
        public IActionResult Post([FromBody]StudentDTO dto)
        {
            if(dto == null){
                return BadRequest();
            }

            _repo.Add(new Student { Name = dto.Name, Email = dto.Email });
            return Ok();
        }

        // PUT api/students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]StudentDTO dto)
        {
            if(dto != null){
                var student = new Student { Id = id, Name = dto.Name, Email = dto.Email };
                if(_repo.Find(id) != null){
                    if(await _repo.Update(student))
                        return Ok();
                } else {
                    if(await _repo.Add(student))
                        return Ok();
                }
            }
            return StatusCode(500, "Error handling our request");
        }

        // DELETE api/students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(!await _repo.Remove(id)){
                return NotFound();  // Bad Shit bro
            }
            return Ok();
        }
    }
}
