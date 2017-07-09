using System;
using System.Threading.Tasks;
using API.Data.Contracts;
using API.Models;
using API.TransferModels.InputModels;
using API.TransferModels.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class GroupsController : Controller
    {
        private readonly IGroupRepository _repo;
        private readonly GroupsSirenHto _rep;

        public GroupsController(
            IGroupRepository repo,
            GroupsSirenHto representation)
        {
            _repo = repo;
            _rep = representation;
        }

        [HttpGet("{id}/students", Name=Routes.GroupStudentsList)]
        public IActionResult GroupStudentsList(int Id)
        {
            return StatusCode(501, "Not Implemented");
        }

        [HttpGet("{id}", Name=Routes.GroupEntry)]
        public async Task<IActionResult> Get(int Id)
        {
            Group group = await _repo.FindByIdAsync(Id);
            if(group == null){
                return NotFound(new ProblemJson{
                    Type = "/group-not-found",
                    Status = 404,
                    Title = "Group Not Found",
                    Detail = "The group with the id "+Id+" does not exist or it wasn't found."
                });
            }

            return Ok(_rep.Entity(group));
        }

        
        [HttpPost("", Name=Routes.GroupCreate)]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Post([FromBody]GroupDTO dto)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            Group group = new Group{
                ClassId = dto.ClassId,
                Number = dto.Number
            };

            if(await _repo.AddAsync(group)){
                return Ok();
            }

            throw new Exception("Unable creating group");
        }

        [HttpDelete("{id}", Name=Routes.GroupDelete)]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int Id)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            Group group = await _repo.FindByIdAsync(Id);
            if(group == null){
                return NotFound(new ProblemJson{
                    Type = "/group-not-found",
                    Status = 404,
                    Title = "Group Not Found",
                    Detail = "The group with the id "+Id+" does not exist or it wasn't found."
                });
            }

            if (await _repo.DeleteAsync(group))
            {
                return NoContent();
            }

            throw new Exception("Unable to add group to class" + Id);
        }
    }
}