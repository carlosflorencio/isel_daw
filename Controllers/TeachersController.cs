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
        [HttpGet("{id}")]
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

            await _repo.Add(new Teacher { Name = dto.Name, Email = dto.Email });
            return Ok();
        }

        // PUT api/teachers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]TeacherDTO dto)
        {
            if(dto != null){
                var teacher = new Teacher { Id = id, Name = dto.Name, Email = dto.Email };
                if(_repo.Find(id) != null){
                    if(await _repo.Update(teacher))
                        return Ok();
                } else {
                    if(await _repo.Add(teacher))
                        return Ok();
                }
            }
            return StatusCode(500, "Error handling our request");
        }

        // DELETE api/teachers/5
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
