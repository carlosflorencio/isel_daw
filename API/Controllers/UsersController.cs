using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data.Repositories;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IRepository<User> _repo;

        public UsersController(IRepository<User> repo)
        {
            _repo = repo;
        }

        // GET api/users
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _repo.GetAll();
        }
    }
}
