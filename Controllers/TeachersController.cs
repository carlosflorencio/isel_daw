using System.Collections.Generic;
using System.Threading.Tasks;
using _1617_2_LI41N_G9.Data.Repositories;
using _1617_2_LI41N_G9.Models;
using _1617_2_LI41N_G9.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace _1617_2_LI41N_G9.Controllers
{
    [Route("api/[controller]")]
    public class TeachersController : Controller
    {
        private IRepository<Teacher> _repo;

        public TeachersController(IRepository<Teacher> repo)
        {
            _repo = repo;
        }
        // GET api/teachers
        [HttpGet]
        public IEnumerable<Teacher> Get()
        {
            return _repo.GetAll();
        }

        // GET api/teachers/5
        [HttpGet("{id}", Name="GetTeacher")]
        public IActionResult Get(int id)
        {
            var entity = _repo.Find(id);
            if(entity == null){
                return NotFound();
            }
            return new ObjectResult(entity);
        }

        // POST api/teachers
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TeacherDTO dto)
        {
            if(dto == null){
                return BadRequest();
            }

            var teacher = new Teacher { Name = dto.Name, Email = dto.Email };
            if(await _repo.Add(teacher)){
                return CreatedAtRoute("GetTeacher", new {id = teacher.Id}, teacher);
            } else {
                return StatusCode(500, "Error handling your request");
            }
        }

        // PUT api/teachers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]TeacherDTO dto)
        {
            if(dto != null){
                // Default transaction level -> Read Committed
                var entity = _repo.Find(id);
                if(entity != null){
                    entity.Name = dto.Name;
                    entity.Email = dto.Email;
                    if(await _repo.Update(entity))
                        return NoContent();
                } else {
                    var teacher = new Teacher { Id = id, Name = dto.Name, Email = dto.Email };
                    if(await _repo.Add(teacher))
                        return CreatedAtRoute("GetTeacher", new {id = teacher.Id}, teacher);
                }
            }
            return StatusCode(500, "Error handling your request");
        }

        // DELETE api/teachers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(!await _repo.Remove(id)){
                return NotFound();  // Bad Shit bro
            }
            return NoContent();
        }
    }
}
