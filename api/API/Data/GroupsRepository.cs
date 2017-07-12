using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.Data.Contracts;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class GroupsRepository : GenericRepository<DatabaseContext, Group>, IGroupRepository
    {
        public GroupsRepository(DatabaseContext ctx) : base(ctx)
        {
        }

        public Task<Group> FindByIdAsync(int id)
        {
            return Context.Groups
                .Where(group => group.Id == id)
                .Include(group => group.Students)
                    .ThenInclude(gs => gs.Student)
                .FirstOrDefaultAsync();
        }
    }
}