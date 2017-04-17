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
    public class StudentsController : Controller
    {
        private IRepository<Student> _repo;

        public StudentsController(IRepository<Student> repo)
        {
            _repo = repo;
        }

        // GET api/students
        [HttpGet]
        public async Task<IEnumerable<Student>> Get([FromQuery]string name)
        {
            Func<Student, bool> func = null;
            if(name != null){
                func = t => t.Name.Contains(name);
            }
            return await _repo.GetAll(func);
        }

        // GET api/students/5
        [HttpGet("{id}", Name="GetStudent")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _repo.Find(id);
            if(entity == null){
                return NotFound();
            }
            return new ObjectResult(entity);
        }

        // POST api/students
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]StudentCreationDTO dto)
        {
            if(dto == null){
                return BadRequest();
            }

            var student = CreationToModelMapper.Map(dto);
            if(await _repo.Add(student)){
                return CreatedAtRoute("GetStudent", new { id = student.Id }, student);
            } else {
                return StatusCode(500, "Error handling your request");
            }
        }

        // PUT api/students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]StudentCreationDTO dto)
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
                    var student = CreationToModelMapper.Map(dto);
                    student.Id = id;
                    if(await _repo.Add(student))
                        return CreatedAtRoute("GetStudent", new { id = student.Id }, student);
                }
            }
            return StatusCode(500, "Error handling your request");
        }

        // DELETE api/students/5
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