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

namespace API.Tests.Controllers
{
    // http://docs.mytestedasp.net/tutorial/organizingtests.html
    public class StudentsControllerTest : MyController<StudentsController>
    {

        public StudentsControllerTest() {
            this.WithDbContext(dbContext => dbContext.WithEntities(ctx => {
                var db = ctx as DatabaseContext;
                db.EnsureSeedDataForContext().Wait();
            }));
        }

        [Fact]
        public void Test_GetAll()
        {
            Calling(c => c.Get())
            .ShouldReturn()
            .Ok()
            .WithModelOfType<List<Student>>()
            .Passing(model => model.Count == 3);

        }

    }
}
