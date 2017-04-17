using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data.Repositories;
using API.Mapper;
using API.Models;
using API.Models.CreationDTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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
        public async Task<IEnumerable<Teacher>> Get([FromQuery]string name)
        {
            Func<Teacher, bool> func = null;
            if(name != null){
                func = t => t.Name.Contains(name);
            }
            return await _repo.GetAll(func);
        }

        // GET api/teachers/5
        [HttpGet("{id}", Name="GetTeacher")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _repo.Find(id);
            if(entity == null){
                return NotFound();
            }
            return new ObjectResult(entity);
        }

        // POST api/teachers
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TeacherCreationDTO dto)
        {
            if(dto == null){
                return BadRequest();
            }

            var teacher = CreationToModelMapper.Map(dto);
            if(await _repo.Add(teacher)){
                return CreatedAtRoute("GetTeacher", new {id = teacher.Id}, teacher);
            } else {
                return StatusCode(500, "Error handling your request");
            }
        }

        // PUT api/teachers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]TeacherCreationDTO dto)
        {
            if(dto != null){
                // Default transaction level -> Read Committed
                var entity = await _repo.Find(id);
                if(entity != null){
                    entity.Number = dto.Number;
                    entity.Name = dto.Name;
                    entity.Email = dto.Email;
                    if(await _repo.Update(entity))
                        return NoContent();
                } else {
                    var teacher = CreationToModelMapper.Map(dto);
                    teacher.Id = id;
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
