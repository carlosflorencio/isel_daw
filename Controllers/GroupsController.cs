using System.Collections.Generic;
using DAW_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DAW_API.Controllers
{
    [Route("api/[controller]")]
    public class GroupsController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<Group> Get()
        {
            return null;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Group Get(int id)
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
