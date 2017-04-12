using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1617_2_LI41N_G9.Data.Repositories;
using _1617_2_LI41N_G9.Models;
using _1617_2_LI41N_G9.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace _1617_2_LI41N_G9.Controllers
{
    [Route("api/[controller]")]
    public class CoursesController : Controller
    {
        private IRepository<Course> _repo;

        public CoursesController(IRepository<Course> repo)
        {
            _repo = repo;
        }

        // GET api/courses
        [HttpGet]
        public async Task<IEnumerable<Course>> Get([FromQuery]string acr)
        {
            if(acr != null){
                Predicate<Course> pred = (c) => c.Acronym.Contains(acr) ;
            }
            var result = await _repo.GetAll();
            return result;
        }

        // GET api/courses/5
        [HttpGet("{id}", Name="GetCourse")]
        public async Task<IActionResult> Get(int id)
        {
            var course = await _repo.Find(id);
            if(course == null)
                return NotFound();

            return new ObjectResult(course);
        }

        // POST api/courses
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CourseDTO dto)
        {
            if(dto == null){
                return BadRequest();
            }

            var course = new Course { Name = dto.Name, Acronym = dto.Acronym, CoordinatorId = dto.CoordinatorId };

            if(await _repo.Add(course)){
                return CreatedAtRoute("GetCourse", new { id = course.Id }, course);
            } else {
                return StatusCode(500, "Error handling your request");
            }
        }

        // PUT api/courses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]CourseDTO dto)
        {
            //TODO: What if coordinator does not exist?

            if(dto != null){
                // Default transaction level -> Read Committed
                var entity = await _repo.Find(id);
                if(entity != null){
                    entity.Name = dto.Name;
                    entity.Acronym = dto.Acronym;
                    entity.CoordinatorId = dto.CoordinatorId;
                    if(await _repo.Update(entity))
                        return NoContent();
                } else {
                    var course = new Course { Id = id, Name = dto.Name, Acronym = dto.Acronym, CoordinatorId = dto.CoordinatorId };
                    if(await _repo.Add(course))
                        return CreatedAtRoute("GetCourse", new { id = id }, course);
                }
            }
            return StatusCode(500, "Error hanlding your request");
        }

        // DELETE api/courses/5
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
