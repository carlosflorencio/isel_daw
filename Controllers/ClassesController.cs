using System.Collections.Generic;
using _1617_2_LI41N_G9.Models;
using _1617_2_LI41N_G9.Models.CreationDTO;
using Microsoft.AspNetCore.Mvc;

namespace _1617_2_LI41N_G9.Controllers
{
    [Route("api/[controller]")]
    public class ClassesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<Class> Get()
        {
            return null;
        }

        // GET api/classes/1617V
        [HttpGet("{sem}")]
        public Class GetClassesBySemester(string sem)
        {
            return null;
        }

        // GET api/classes/1617V/courses/1
        [HttpGet("{sem}/courses/{courseId}")]
        public Class GetClassesOfCourseBySemester(string sem, int courseId)
        {
            return null;
        }

        // POST api/classes/1617V/courses/1
        [HttpPost]
        public void Post([FromBody]ClassCreationDTO dto)
        {
        }

        // POST api/classes/1617V/courses/1/teachers/1
        [HttpPost]
        public void PostTeacherToClass([FromBody]ClassCreationDTO dto)
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
