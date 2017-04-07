using System.Collections.Generic;
using _1617_2_LI41N_G9.Models;
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

        // GET api/values/5
        [HttpGet("{id}")]
        public Class Get(int id)
        {
            return null;
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
