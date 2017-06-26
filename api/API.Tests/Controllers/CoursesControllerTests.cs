using System.Threading.Tasks;
using API.Controllers;
using API.Data;
using API.Extensions;
using API.Models;
using API.TransferModels.InputModels;
using MyTested.AspNetCore.Mvc;
using Xunit;
using API.Siren;

namespace API.Tests.Controllers
{
    // http://docs.mytestedasp.net/tutorial/organizingtests.html
    public class CoursesControllerTest : MyController<CoursesController>
    {
        public CoursesControllerTest()
        {
            // Seed DB data for all tests
            this.WithDbContext(dbContext => dbContext.WithEntities(ctx =>
            {
                var db = ctx as DatabaseContext;
                db.EnsureSeedDataForContext();
            }));
        }

        [Fact]
        public async Task Test_Post_Course()
        {
            CourseDTO dto = new CourseDTO{
                Name="Teste",
                Acronym="T",
                CoordinatorId=1456
            };
            this.Calling(c => c.Post(dto))
                .ShouldReturn()
                .Created();

        }
    }
}