using System.Threading.Tasks;
using API.Controllers;
using API.Data;
using API.Extensions;
using API.Models;
using API.TransferModels.InputModels;
using MyTested.AspNetCore.Mvc;
using Xunit;
using API.Siren;
using FluentSiren.Models;

namespace API.Tests.Controllers
{
    // http://docs.mytestedasp.net/tutorial/organizingtests.html
    public class ClassesControllerTests : MyController<ClassesController>
    {
        public ClassesControllerTests()
        {
            // Seed DB data for all tests
            this.WithDbContext(dbContext => dbContext.WithEntities(ctx =>
            {
                var db = ctx as DatabaseContext;
                db.EnsureSeedDataForContext();
            }));
        }

        [Fact]
        public async Task Test_Post_Class()
        {
//            ClassDTO dto = new ClassDTO
//            {
//                Name = "Teste",
//                MaxGroupSize = 3,
//                AutoEnrollment = true,
//                SemesterId = 1,
//                CourseId = 1
//            };
//            this.Calling(c => c.Post(dto))
//                .ShouldReturn()
//                .Created();

        }

        [Fact]
        public async Task Test_Get_Class()
        {
//            this.Calling(c => c.Get(1))
//                .ShouldReturn()
//                .Ok();
        }

        [Fact]
        public async Task Test_Get_Class_Teachers()
        {
//            this.Calling(c => c.ClassTeachers(1, new ListQueryStringDto()))
//                .ShouldReturn()
//                .Ok();
        }
    }
}