using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data.Contracts;
using API.Models;
using API.Services;
using API.TransferModels.InputModels;
using API.TransferModels.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ClassesController : Controller
    {
        private IClassRepository _repo;

        private readonly ClassesSirenHto _classesRep;
        private readonly GroupsSirenHto _groupsRep;
        private readonly TeachersSirenHto _teachersRep;
        private readonly StudentsSirenHto _studentsRep;

        public ClassesController(
            IClassRepository repo,
            ClassesSirenHto classesRepresentation,
            TeachersSirenHto teachersRepresentation,
            StudentsSirenHto studentsRepresentation,
            GroupsSirenHto groupsRepresentation)
        {
            _repo = repo;
            _classesRep = classesRepresentation;
            _teachersRep = teachersRepresentation;
            _studentsRep = studentsRepresentation;
            _groupsRep = groupsRepresentation;
        }

        [HttpGet("{id}", Name=Routes.ClassEntry)]
        public async Task<IActionResult> Get(int Id)
        {

            Class c = await _repo.GetByIdAsync(Id);

            if(c == null){
                return NotFound();
            }

            return Ok(_classesRep.Entity(c));
        }

        [HttpPost("", Name=Routes.ClassCreate)]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Post([FromBody]ClassDTO dto)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            //TODO: AutoMapper
            Class c = new Class{
                Name = dto.Name,
                MaxGroupSize = dto.MaxGroupSize,
                AutoEnrollment = dto.AutoEnrollment,
                SemesterId = dto.SemesterId,
                CourseId = dto.CourseId,
            };

            if(await _repo.AddAsync(c)){
                return CreatedAtRoute(
                    Routes.ClassEntry,
                    new { Id = c.Id },
                    _classesRep.Entity(c));
            }

            throw new Exception("Unable to add Class");
        }

        [HttpPut("{id}", Name=Routes.ClassEdit)]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Put(int Id, [FromBody]ClassDTO dto)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            Class c = await _repo.GetByIdAsync(Id);
            if(c == null)
            {
                return NotFound();
            }

            //TODO: Mapper
            c.Name = dto.Name;
            c.MaxGroupSize = dto.MaxGroupSize;
            c.AutoEnrollment = dto.AutoEnrollment;
            c.SemesterId = dto.SemesterId;
            c.CourseId = dto.CourseId;

            if(await _repo.EditAsync(c)){
                return Ok(_classesRep.Entity(c));
            }

            throw new Exception("Unbale to edit Class " + Id);
        }

        [HttpDelete("{id}", Name=Routes.ClassDelete)]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int Id)
        {
            Class c = await _repo.GetByIdAsync(Id);
            if(c == null)
            {
                return NotFound();
            }

            if(await _repo.DeleteAsync(c)){
                return NoContent();
            }

            throw new Exception("Unable to delete Class " + Id);
        }

        [HttpGet("{id}/groups", Name=Routes.ClassGroupsList)]
        public async Task<IActionResult> ClassGroups(int Id, [FromQuery]ListQueryStringDto query)
        {
            PagedList<Group> groups = await _repo.GetClassGroups(Id, query);

            return Ok(_groupsRep.Collection(groups, query));
        }

        [HttpGet("{id}/teachers", Name=Routes.ClassTeachersList)]
        public async Task<IActionResult> ClassTeachers(int Id, [FromQuery]ListQueryStringDto query)
        {
            PagedList<Teacher> teachers = await _repo.GetClassTeachers(Id, query);

            return Ok(_teachersRep.Collection(teachers, query));
        }

        [HttpGet("{id}/students", Name=Routes.ClassStudentsList)]
        public async Task<IActionResult> ClassStudents(int Id, [FromQuery]ListQueryStringDto query)
        {
            PagedList<Student> students = await _repo.GetClassStudents(Id, query);

            return Ok(_studentsRep.Collection(students, query));
        }

        [HttpPost("{id}/students", Name=Routes.ClassParticipantAdd)]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> AddParticipant(int Id, [FromBody]StudentDTO dto)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            Class c = await _repo.GetByIdAsync(Id);
            if(c == null){
                return NotFound();
            }

            if(await _repo.AddParticipantTo(c, dto.Number)){
                return Ok();    //TODO: What to return...
            }

            throw new Exception("Unable to add participant " + dto.Number);
        }

        [HttpPost("{id}/teachers", Name=Routes.ClassTeacherAdd)]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> AddTeacher(int Id, [FromBody]TeacherDTO dto)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            Class c = await _repo.GetByIdAsync(Id);
            if(c == null){
                return NotFound();
            }

            if(await _repo.AddTeacherTo(c, dto.Number)){
                return Ok();    //TODO: What to return...
            }

            throw new Exception("Unable to add participant " + dto.Number);
        }

        [HttpPost("{id}/groups", Name=Routes.ClassGroupAdd)]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> AddGroup(int Id)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            Class c = await _repo.GetByIdAsync(Id);
            if(c == null){
                return NotFound();
            }

            if(await _repo.AddGroupTo(c)){
                return Ok();    //TODO: What to return...
            }

            throw new Exception("Unable to add group to class" + Id);
        }
    }
}