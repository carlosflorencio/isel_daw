using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Controllers;
using API.Data;
using API.Models;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MyTested.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc.Builders.Contracts.Data;
using Xunit;
using API.Extensions;
using API.TransferModels.InputModels;
using FluentSiren.Models;

namespace API.Tests.Controllers
{
    // http://docs.mytestedasp.net/tutorial/organizingtests.html
    public class StudentsControllerTest : MyController<StudentsController>
    {

        public StudentsControllerTest()
        {
            this.WithDbContext(dbContext => dbContext.WithEntities(ctx =>
            {
                var db = ctx as DatabaseContext;
                db.EnsureSeedDataForContext();
            }));
        }

        [Fact]
        public void GetAllStudentsWithAction()
        {
            this.WithRouteData()
             .WithAuthenticatedUser(u => u.InRole(Roles.Admin))
            .Calling(c => c.List(new ListQueryStringDto() {}))
            .ShouldReturn()
            .Ok()
            .WithModelOfType<Entity>()
            .Passing(model => model.Entities.Count > 0 && model.Actions.Count == 1);

        }

    }
}
