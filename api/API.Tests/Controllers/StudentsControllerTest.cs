using System.Threading.Tasks;
using API.Controllers;
using API.Data;
using MyTested.AspNetCore.Mvc;
using Xunit;
using API.Extensions;
using API.TransferModels.InputModels;
using FluentSiren.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace API.Tests.Controllers
{
    // http://docs.mytestedasp.net/tutorial/organizingtests.html
    public class StudentsControllerTest : MyController<StudentsController>
    {

        public StudentsControllerTest()
        {
            // Seed DB data for all tests
            this.WithDbContext(dbContext => dbContext.WithEntities(ctx =>
            {
                var db = ctx as DatabaseContext;
                db.EnsureSeedDataForContext();
            }));
        }

        /*
        |--------------------------------------------------------------------------
        | List
        |--------------------------------------------------------------------------
        */

        // [Fact]
        // public async Task Test_Get_Students_Without_Auth()
        // {
        //     // Arrange
        //     var server = new TestServer(new WebHostBuilder().UseStartup<TestStartup>());
        //     var client = server.CreateClient();

        //     // Act
        //     var response = await client.GetAsync("api/students");

        //     // Assert
        //     Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        // }

        [Fact]
        public void Get_All_Students_With_Admin_Role()
        {
            // the action to add a student should be returned
            this.WithRouteData()
            .WithAuthenticatedUser(u => u.InRole(Roles.Admin))
            .Calling(c => c.List(new ListQueryStringDto() { }))
            .ShouldReturn()
            .Ok()
            .WithModelOfType<SirenEntity>()
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
            .WithModelOfType<SirenEntity>()
            .Passing(
                model =>
                    model.Entities.Count > 0 &&
                    model.Actions == null);
        }

        /*
        |--------------------------------------------------------------------------
        | Item
        |--------------------------------------------------------------------------
        */

        [Fact]
        public void Get_Student_With_Student_Role()
        {
            // the action to add a student should not be returned
            this.WithRouteData()
            .WithAuthenticatedUser(u => u.InRole(Roles.Student))
            .Calling(c => c.Get(39250))
            .ShouldReturn()
            .Ok()
            .WithModelOfType<SirenEntity>()
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
            .WithModelOfType<SirenEntity>()
            .Passing(
                model =>
                    model.Properties["email"].Equals("carlos@gmail.com") &&
                    model.Actions.Count > 0);
        }

    }
}
