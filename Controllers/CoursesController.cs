using System;
using System.Collections.Generic;
using System.Linq;
using _1617_2_LI41N_G9.Data.Repositories;
using _1617_2_LI41N_G9.Models;
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
        public IEnumerable<Course> Get([FromQuery]string acr)
        {
            if(acr != null){
                Predicate<Course> pred = (c) => c.Acronym.Contains(acr) ;
            }
            var result = _repo.GetAll();
            return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var course = _repo.Find(id);
            if(course == null)
                return NotFound();

            return new ObjectResult(course);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
