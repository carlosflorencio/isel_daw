using System;
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
    public class TeachersSirenHto
    {
        private readonly IUrlHelper _url;
        private readonly ActionContext _context;

        public TeachersSirenHto(IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor)
        {
            _context = actionContextAccessor.ActionContext;
            _url = urlHelperFactory.GetUrlHelper(_context);
        }

        public Entity Collection(PagedList<Teacher> items, ListQueryStringDto query)
        {
            var entity = new EntityBuilder()
                    .WithClass("teacher")
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

            foreach (var teacher in items)
            {
                entity
                    .WithSubEntity(new EmbeddedRepresentationBuilder()
                    .WithClass("teacher")
                    .WithRel("item")
                    .WithProperty("number", teacher.Number)
                    .WithProperty("name", teacher.Name)
                    .WithProperty("email", teacher.Email)
                    .WithLink(new LinkBuilder()
                        .WithRel("self")
                        .WithHref(_url.AbsoluteRouteUrl(Routes.TeacherEntry,
                        new { number = teacher.Number })))
                    .WithLink(new LinkBuilder()
                        .WithRel(SirenData.REL_TEACHERS_CLASSES)
                        .WithHref(_url.AbsoluteRouteUrl(Routes.TeacherClassList,
                        new { number = teacher.Number }))));
            }

            if (_context.HttpContext.User.IsInRole(Roles.Admin)) {
                entity
                    .WithAction(new ActionBuilder()
                        .WithName("add-teacher")
                        .WithTitle("Add Teacher")
                        .WithMethod("POST")
                        .WithHref(_url.AbsoluteRouteUrl(Routes.TeacherCreate))
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
                    .WithHref(_url.AbsoluteRouteUrl(Routes.TeacherList)))
                .WithLink(new LinkBuilder()
                    .WithRel("index")
                    .WithHref(_url.AbsoluteRouteUrl(Routes.Index)))
                .WithNavigationLinks(_url, Routes.TeacherList, items.TotalPages, query);

            return entity.Build();
        }

        internal object ClassesCollection(PagedList<Class> items, ListQueryStringDto query)
        {
            var entity = new EntityBuilder()
                    .WithClass("class")
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

            foreach (var item in items)
            {
                entity
                    .WithSubEntity(new EmbeddedRepresentationBuilder()
                    .WithClass("class")
                    .WithRel("item")
                    .WithProperty("name", item.Name)
                    .WithProperty("maxGroupSize", item.MaxGroupSize)
                    .WithProperty("autoEnrollment", item.AutoEnrollment)
                    .WithLink(new LinkBuilder()
                        .WithRel("self")
                        .WithHref(_url.AbsoluteRouteUrl(Routes.ClassEntry,
                        new { id = item.Id }))));
                    //TODO: relations
            }

            //TODO: add actions

            entity
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(_url.AbsoluteRouteUrl(Routes.TeacherClassList)))
                .WithLink(new LinkBuilder()
                    .WithRel("index")
                    .WithHref(_url.AbsoluteRouteUrl(Routes.Index)))
                .WithNavigationLinks(_url, Routes.TeacherClassList, items.TotalPages, query);

            return entity.Build();

        }

        public Entity Entity(Teacher teacher)
        {
            var entity = new EntityBuilder()
                .WithClass("student")
                .WithProperty("number", teacher.Number)
                .WithProperty("name", teacher.Name)
                .WithProperty("email", teacher.Email)
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(_url.AbsoluteRouteUrl(Routes.TeacherEntry,
                                                    new { number = teacher.Number })))
                .WithLink(new LinkBuilder()
                    .WithRel(SirenData.REL_TEACHERS_CLASSES)
                    .WithHref(_url.AbsoluteRouteUrl(Routes.TeacherClassList,
                                                    new { number = teacher.Number })));


            if (_context.HttpContext.User.IsInRole(Roles.Admin)) {
                entity
                    .WithAction(
                        new ActionBuilder()
                            .WithName("edit-student")
                            .WithTitle("Edit Student")
                            .WithMethod("PUT")
                            .WithHref(_url.AbsoluteRouteUrl(Routes.TeacherEdit,
                                                            new { number = teacher.Number }))
                            .WithType("application/json")
                            .WithField(
                                new FieldBuilder()
                                    .WithName("name")
                                    .WithType("text")
                                    .WithValue(teacher.Name))
                            .WithField(
                                new FieldBuilder()
                                    .WithName("email")
                                    .WithType("email")
                                    .WithValue(teacher.Email)))
                    .WithAction(
                        new ActionBuilder()
                            .WithName("delete-student")
                            .WithTitle("Delete Student")
                            .WithMethod("DELETE")
                            .WithHref(_url.AbsoluteRouteUrl(Routes.TeacherDelete,
                                                            new { number = teacher.Number }))
                            );
            }

            return entity.Build();
        }
    }
}