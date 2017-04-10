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

            var course = new Course { Coordinator = new Teacher { Id = dto.CoordinatorId }, Name = dto.Name, Acronym = dto.Acronym };

            if(await _repo.Add(course)){
                return CreatedAtRoute("GetCourse", new { id = course.Id }, course);
            } else {
                return StatusCode(500, "Error handling your request");
            }
        }

        // PUT api/courses/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]string value)
        {
            return null;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return null;
        }
    }
}
