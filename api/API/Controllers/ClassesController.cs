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
        private IClassRepository _classesRepo;
        private readonly ClassesSirenHto _classesRep;
        private readonly ClassGroupsSirenHto _groupsRep;
        private readonly ClassTeachersSirenHto _teachersRep;
        private readonly ClassStudentsSirenHto _studentsRep;

        public ClassesController(
            IClassRepository classesRepo,
            ClassesSirenHto classesRepresentation,
            ClassTeachersSirenHto teachersRepresentation,
            ClassStudentsSirenHto studentsRepresentation,
            ClassGroupsSirenHto groupsRepresentation)
        {
            _classesRepo = classesRepo;
            _classesRep = classesRepresentation;
            _teachersRep = teachersRepresentation;
            _studentsRep = studentsRepresentation;
            _groupsRep = groupsRepresentation;
        }

        [HttpGet("{id}", Name=Routes.ClassEntry)]
        public async Task<IActionResult> Get(int Id)
        {

            Class c = await _classesRepo.GetByIdAsync(Id);

            if(c == null){
                return NotFound(new ProblemJson{
                    Type = "/class-not-found",
                    Status = 404,
                    Title = "Class Not Found",
                    Detail = "The class with the id "+Id+" does not exist or it wasn't found."
                });
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

            if(await _classesRepo.AddAsync(c)){
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

            Class c = await _classesRepo.GetByIdAsync(Id);
            if(c == null)
            {
                return NotFound(new ProblemJson{
                    Type = "/class-not-found",
                    Status = 404,
                    Title = "Class Not Found",
                    Detail = "The class with the id "+Id+" does not exist or it wasn't found."
                });
            }

            //TODO: Mapper
            c.Name = dto.Name;
            c.MaxGroupSize = dto.MaxGroupSize;
            c.AutoEnrollment = dto.AutoEnrollment;
            c.SemesterId = dto.SemesterId;
            c.CourseId = dto.CourseId;

            if(await _classesRepo.EditAsync(c)){
                return Ok(_classesRep.Entity(c));
            }

            throw new Exception("Unbale to edit Class " + Id);
        }

        [HttpDelete("{id}", Name=Routes.ClassDelete)]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int Id)
        {
            Class c = await _classesRepo.GetByIdAsync(Id);
            if(c == null)
            {
                return NotFound(new ProblemJson{
                    Type = "/class-not-found",
                    Status = 404,
                    Title = "Class Not Found",
                    Detail = "The class with the id "+Id+" does not exist or it wasn't found."
                });
            }

            if(await _classesRepo.DeleteAsync(c)){
                return NoContent();
            }

            throw new Exception("Unable to delete Class " + Id);
        }

        [HttpGet("{id}/groups", Name=Routes.ClassGroupsList)]
        public async Task<IActionResult> ClassGroups(int Id, [FromQuery]ListQueryStringDto query)
        {
            Class c = await _classesRepo.GetByIdAsync(Id);
            if(c == null){
                return NotFound(new ProblemJson{
                    Type = "/class-not-found",
                    Status = 404,
                    Title = "Class Not Found",
                    Detail = "The class with the id "+Id+" does not exist or it wasn't found."
                });
            }
            
            PagedList<Group> groups = await _classesRepo.GetClassGroups(Id, query);

            return Ok(_groupsRep.WeakCollection(Id, groups, query));
        }

        [HttpGet("{id}/teachers", Name=Routes.ClassTeachersList)]
        public async Task<IActionResult> ClassTeachers(int Id, [FromQuery]ListQueryStringDto query)
        {
            Class c = await _classesRepo.GetByIdAsync(Id);
            if(c == null){
                return NotFound(new ProblemJson{
                    Type = "/class-not-found",
                    Status = 404,
                    Title = "Class Not Found",
                    Detail = "The class with the id "+Id+" does not exist or it wasn't found."
                });
            }

            PagedList<Teacher> teachers = await _classesRepo.GetClassTeachers(Id, query);

            return Ok(_teachersRep.WeakCollection(Id, teachers, query));
        }

        [HttpGet("{id}/students", Name=Routes.ClassStudentsList)]
        public async Task<IActionResult> ClassStudents(int Id, [FromQuery]ListQueryStringDto query)
        {
            Class c = await _classesRepo.GetByIdAsync(Id);
            if(c == null){
                return NotFound(new ProblemJson{
                    Type = "/class-not-found",
                    Status = 404,
                    Title = "Class Not Found",
                    Detail = "The class with the id "+Id+" does not exist or it wasn't found."
                });
            }

            PagedList<Student> students = await _classesRepo.GetClassStudents(Id, query);

            return Ok(_studentsRep.WeakCollection(Id, students, query));
        }

        [HttpPost("{id}/students", Name=Routes.ClassParticipantAdd)]
        [Authorize(Roles = Roles.Teacher)]
        public async Task<IActionResult> AddStudent(int Id, [FromBody]StudentDTO dto)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            Class c = await _classesRepo.GetByIdAsync(Id);
            if(c == null){
                return NotFound(new ProblemJson{
                    Type = "/class-not-found",
                    Status = 404,
                    Title = "Class Not Found",
                    Detail = "The class with the id "+Id+" does not exist or it wasn't found."
                });
            }

            if(await _classesRepo.AddStudentTo(c, dto.Number)){
                return Ok();    //TODO: What to return...
            }

            throw new Exception("Unable to add participant " + dto.Number);
        }

        [HttpDelete("{id}/students/{studentId}", Name=Routes.ClassParticipantRemove)]
        [Authorize(Roles = Roles.Teacher)]
        public async Task<IActionResult> RemoveStudent(int Id, int studentId)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            Class c = await _classesRepo.GetByIdAsync(Id);
            if(c == null){
                return NotFound(new ProblemJson{
                    Type = "/class-not-found",
                    Status = 404,
                    Title = "Class Not Found",
                    Detail = "The class with the id "+Id+" does not exist or it wasn't found."
                });
            }

            if(await _classesRepo.RemoveStudentFrom(new Class {Id = Id}, studentId)){
                return NoContent();
            }

            throw new Exception("Unable to remove student " + studentId);
        }

        [HttpPost("{id}/teachers", Name=Routes.ClassTeacherAdd)]
        [Authorize(Roles = Roles.Teacher)]
        public async Task<IActionResult> AddTeacher(int Id, [FromBody]TeacherDTO dto)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            Class c = await _classesRepo.GetByIdAsync(Id);
            if(c == null){
                return NotFound(new ProblemJson{
                    Type = "/class-not-found",
                    Status = 404,
                    Title = "Class Not Found",
                    Detail = "The class with the id "+Id+" does not exist or it wasn't found."
                });
            }

            if(await _classesRepo.AddTeacherTo(c, dto.Number)){
                return Ok();    //TODO: What to return...
            }

            throw new Exception("Unable to add teacher " + dto.Number);
        }

        [HttpDelete("{id}/teachers/{teacherId}", Name=Routes.ClassTeacherRemove)]
        [Authorize(Roles = Roles.Teacher)]
        public async Task<IActionResult> RemoveTeacher(int Id, int teacherId)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            Class c = await _classesRepo.GetByIdAsync(Id);
            if(c == null){
                return NotFound(new ProblemJson{
                    Type = "/class-not-found",
                    Status = 404,
                    Title = "Class Not Found",
                    Detail = "The class with the id "+Id+" does not exist or it wasn't found."
                });
            }

            if(await _classesRepo.RemoveTeacherFrom(c, teacherId)){
                return NoContent();
            }

            throw new Exception("Unable to remove teacher " + teacherId);
        }
    }
}