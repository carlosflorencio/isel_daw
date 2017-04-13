using System.Collections.Generic;
using System.Threading.Tasks;
using DAW_API.Data.Repositories;
using DAW_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DAW_API.Controllers
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
