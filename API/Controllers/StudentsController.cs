using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Contracts;
using API.TransferModels.InputModels;
using API.TransferModels.ResponseModels;
using FluentSiren.Builders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        public enum Actions
        {
            All
        }

        private readonly IStudentRepository _repo;
        private readonly StudentsSirenHto _representation;

        public StudentsController(IStudentRepository repo, StudentsSirenHto representation)
        {
            _repo = repo;
            _representation = representation;
        }

        // GET: api/students
        [HttpGet("", Name = Routes.StudentList)]
        public async Task<IActionResult> List([FromQuery] ListQueryStringDto query)
        {
            var students = await _repo.GetAllPaginatedAsync(query);

            var result = _representation.Collection(students, query);

            return Ok(result);
        }

        // GET api/students/39250
        [HttpGet("{number}", Name = Routes.StudentEntry)]
        public string Get(int number)
        {
            return "value";
        }

        // POST api/students
        [HttpPost("", Name = Routes.StudentCreate)]
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


        // GET: api/students/{number}/classes
        [HttpGet("{number}/classes", Name = Routes.StudentClassList)]
        public string Classes(int number, [FromQuery] ListQueryStringDto query)
        {
            return "value";
        }

        // GET: api/students/{number}/groups
        [HttpGet("{number}/groups", Name = Routes.StudentGroupList)]
        public string Groups(int number, [FromQuery] ListQueryStringDto query)
        {
            return "value";
        }
    }
}
