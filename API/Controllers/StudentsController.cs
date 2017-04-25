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
        [HttpGet("{number:int}", Name = Routes.StudentEntry)]
        public async Task<IActionResult> Get(int number) {
            var student = await _repo.GetByNumberAsync(number);

            return Ok(_representation.Entity(student));
        }

        // POST api/students
        [HttpPost("", Name = Routes.StudentCreate)]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{number:int}", Name = Routes.StudentEdit)]
        public void Put(int number, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{number:int}", Name = Routes.StudentDelete)]
        public void Delete(int number)
        {
        }


        // GET: api/students/{number}/classes
        [HttpGet("{number:int}/classes", Name = Routes.StudentClassList)]
        public string Classes(int number, [FromQuery] ListQueryStringDto query)
        {
            return "value";
        }

        // GET: api/students/{number}/groups
        [HttpGet("{number:int}/groups", Name = Routes.StudentGroupList)]
        public string Groups(int number, [FromQuery] ListQueryStringDto query)
        {
            return "value";
        }
    }
}
