using System.Collections.Generic;
using _1617_2_LI41N_G9.Data.Repositories;
using _1617_2_LI41N_G9.Models;
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
        // GET api/values
        [HttpGet]
        public IEnumerable<Teacher> Get()
        {
            return null;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Teacher Get(int id)
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
