﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Contracts;
using API.Models;
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

            if(student == null){
                return NotFound();
            }

            return Ok(_representation.Entity(student));
        }

        
        [HttpPost("", Name = Routes.StudentCreate)]
        //[Authorize(Roles = Roles.Admin)] //TODO: Authorization only for admins
        public async Task<IActionResult> Post([FromBody]StudentDTO dto)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            //TODO: AutoMapper
            Student student = new Student{
                Number = dto.Number,
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password
            };

            if(!await _repo.AddAsync(student)){
                throw new Exception("Unable to add student");
            }

            return CreatedAtRoute(
                Routes.StudentEntry,
                new {number = student.Number},
                _representation.Entity(student)
            );
        }

        [HttpPut("{number:int}", Name = Routes.StudentEdit)]
        //[Authorize(Roles = Roles.Admin)] //TODO: Authorization only for admins
        public async Task<IActionResult> Put(int Number, [FromBody]StudentDTO dto)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            Student student = await _repo.GetByNumberAsync(Number);
            if(student == null){
                return NotFound();
            }

            //TODO: AutoMapper
            student.Number = dto.Number;
            student.Name = dto.Name;
            student.Email = dto.Email;
            student.Password = dto.Password;

            if(!await _repo.EditAsync(student)){
                throw new Exception("Unable to edit student " + Number);
            }
            
            return Ok(_representation.Entity(student));
            //return NoContent();
        }

        [HttpDelete("{number:int}", Name = Routes.StudentDelete)]
        //[Authorize(Roles = Roles.Admin)] //TODO: Authorization only for admins
        public async Task<IActionResult> Delete(int Number)
        {
            Student student = await _repo.GetByNumberAsync(Number);

            if(student == null){
                return NotFound();
            }

            if(await _repo.DeleteAsync(student))
            {
                return NoContent();
            }
            
            throw new Exception("Unable to delete student " + Number);
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
