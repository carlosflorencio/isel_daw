using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAW_API.Data.Repositories;
using DAW_API.Mapper;
using DAW_API.Models;
using DAW_API.Models.CreationDTO;
using Microsoft.AspNetCore.Mvc;

namespace DAW_API.Controllers
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
        public async Task<IActionResult> Post([FromBody]CourseCreationDTO dto)
        {
            //            if(dto == null){
            //                return BadRequest();
            //            }
            //
            //            var course = CreationToModelMapper.Map(dto);
            //
            //            if(await _repo.Add(course)){
            //                return CreatedAtRoute("GetCourse", new { id = course.Id }, course);
            //            } else {
            //                return StatusCode(500, "Error handling your request");
            //            }

            return StatusCode(500, "Error handling your request");
        }

        // PUT api/courses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]CourseCreationDTO dto)
        {
            //TODO: What if coordinator does not exist?

//            if(dto != null){
//                // Default transaction level -> Read Committed
//                var entity = await _repo.Find(id);
//                if(entity != null){
//                    entity.Name = dto.Name;
//                    entity.Acronym = dto.Acronym;
//                    entity.CoordinatorId = dto.CoordinatorId;
//                    if(await _repo.Update(entity))
//                        return NoContent();
//                } else {
//                    var course = CreationToModelMapper.Map(dto);
//                    course.Id = id;
//                    if(await _repo.Add(course))
//                        return CreatedAtRoute("GetCourse", new { id = id }, course);
//                }
//            }
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
