using System.Collections.Generic;
using _1617_2_LI41N_G9.Data.Repositories;
using _1617_2_LI41N_G9.Models;
using Microsoft.AspNetCore.Mvc;

namespace _1617_2_LI41N_G9.Controllers
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
        public IEnumerable<User> Get()
        {
            return _repo.GetAll();
        }
    }
}
