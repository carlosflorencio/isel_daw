using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public void Get_All_Students_With_Admin_Role()
        {
            // the action to add a student should be returned
            this.WithRouteData()
            .WithAuthenticatedUser(u => u.InRole(Roles.Admin))
            .Calling(c => c.List(new ListQueryStringDto() { }))
            .ShouldReturn()
            .Ok()
            .WithModelOfType<Entity>()
            .Passing(
                model =>
                    model.Entities.Count > 0 &&
                    model.Actions.Count == 1);
        }

        [Fact]
        public void Get_All_Students_With_Student_Role()
        {
            // the action to add a student should not be returned
            this.WithRouteData()
            .WithAuthenticatedUser(u => u.InRole(Roles.Student))
            .Calling(c => c.List(new ListQueryStringDto() { }))
            .ShouldReturn()
            .Ok()
            .WithModelOfType<Entity>()
            .Passing(
                model =>
                    model.Entities.Count > 0 &&
                    model.Actions == null);
        }

        [Fact]
        public void Get_Student_With_Student_Role()
        {
            // the action to add a student should not be returned
            this.WithRouteData()
            .WithAuthenticatedUser(u => u.InRole(Roles.Student))
            .Calling(c => c.Get(39250))
            .ShouldReturn()
            .Ok()
            .WithModelOfType<Entity>()
            .Passing(
                model =>
                    model.Properties["email"].Equals("carlos@gmail.com") &&
                    model.Actions == null);
        }

        [Fact]
        public void Get_Student_With_Admin_Role()
        {
            // the action to add a student should not be returned
            this.WithRouteData()
            .WithAuthenticatedUser(u => u.InRole(Roles.Admin))
            .Calling(c => c.Get(39250))
            .ShouldReturn()
            .Ok()
            .WithModelOfType<Entity>()
            .Passing(
                model =>
                    model.Properties["email"].Equals("carlos@gmail.com") &&
                    model.Actions.Count > 0);
        }

    }
}
