using API.Controllers;
using API.Extensions;
using API.Models;
using API.Services;
using API.TransferModels.InputModels;
using FluentSiren.Builders;
using FluentSiren.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace API.TransferModels.ResponseModels
{
    public class StudentsSirenHto
    {
        private readonly IUrlHelper _url;
        private readonly ActionContext _context;

        public StudentsSirenHto(IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor)
        {
            _context = actionContextAccessor.ActionContext;
            _url = urlHelperFactory.GetUrlHelper(_context);
        }

        public Entity Collection(PagedList<Student> items, ListQueryStringDto query)
        {
            var entity = new EntityBuilder()
                    .WithClass("student")
                    .WithClass("collection")
                    .WithProperty("totalCount", items.TotalCount)
                    .WithProperty("totalPages", items.TotalPages)
                    .WithProperty("currentPage", items.CurrentPage)
                    .WithProperty("pageSize", items.PageSize);

            if (!string.IsNullOrEmpty(query.Search))
            {
                entity
                    .WithProperty("search", query.Search);
            }

            foreach (var student in items)
            {
                entity
                    .WithSubEntity(new EmbeddedRepresentationBuilder()
                    .WithClass("student")
                    .WithRel("item")
                    .WithProperty("number", student.Number)
                    .WithProperty("name", student.Name)
                    .WithProperty("email", student.Email)
                    .WithLink(new LinkBuilder()
                        .WithRel("self")
                        .WithHref(_url.AbsoluteRouteUrl(Routes.StudentEntry,
                        new { number = student.Number })))
                    .WithLink(new LinkBuilder()
                        .WithRel(SirenData.REL_STUDENTS_CLASSES)
                        .WithHref(_url.AbsoluteRouteUrl(Routes.StudentClassList,
                        new { number = student.Number })))
                    .WithLink(new LinkBuilder()
                        .WithRel(SirenData.REL_STUDENTS_GROUPS)
                        .WithHref(_url.AbsoluteRouteUrl(Routes.StudentGroupList,
                        new { number = student.Number }))));
            }

            if (_context.HttpContext.User.IsInRole(Roles.Admin)) {
                entity
                    .WithAction(new ActionBuilder()
                        .WithName("add-student")
                        .WithTitle("Add Student")
                        .WithMethod("POST")
                        .WithHref(_url.AbsoluteRouteUrl(Routes.StudentCreate))
                        .WithType("application/json")
                        .WithField(new FieldBuilder()
                                        .WithName("number")
                                        .WithType("number"))
                        .WithField(new FieldBuilder()
                                        .WithName("name")
                                        .WithType("text"))
                        .WithField(new FieldBuilder()
                                        .WithName("email")
                                        .WithType("email"))
                        .WithField(new FieldBuilder()
                                        .WithName("password")
                                        .WithType("password")));
            }

            entity
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(_url.AbsoluteRouteUrl(Routes.StudentList)))
                .WithLink(new LinkBuilder()
                    .WithRel("index")
                    .WithHref(_url.AbsoluteRouteUrl(Routes.Index)))
                .WithNavigationLinks(_url, Routes.StudentList, items.TotalPages, query);

            return entity.Build();
        }
    }
}
