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

        public SirenEntity Collection(PagedList<Teacher> items, ListQueryStringDto query)
        {
//            var entity = new SirenEntityBuilder()
//                    .WithClass("teacher")
//                    .WithClass("collection")
//                    .WithProperty("totalCount", items.TotalCount)
//                    .WithProperty("totalPages", items.TotalPages)
//                    .WithProperty("currentPage", items.CurrentPage)
//                    .WithProperty("pageSize", items.PageSize);
//
//            if (!string.IsNullOrEmpty(query.Search))
//            {
//                entity
//                    .WithProperty("search", query.Search);
//            }
//
//            foreach (var teacher in items) {
//                var subEntity = new EmbeddedRepresentationBuilder()
//                    .WithClass("teacher")
//                    .WithProperty("number", teacher.Number)
//                    .WithProperty("name", teacher.Name)
//                    .WithProperty("email", teacher.Email)
//                    .WithLink(
//                        new LinkBuilder()
//                            .WithRel("self")
//                            .WithHref(
//                                Url.AbsoluteRouteUrl(
//                                    Routes.TeacherEntry,
//                                    new {number = teacher.Number})))
//                    .WithLink(
//                        new LinkBuilder()
//                            .WithRel(SirenData.REL_TEACHERS_CLASSES)
//                            .WithHref(
//                                Url.AbsoluteRouteUrl(
//                                    Routes.TeacherClassList,
//                                    new {number = teacher.Number})));
//
//                var sub = subEntity as EmbeddedRepresentationBuilder;
//                sub.WithRel("item");
//                entity.WithSubEntity(sub);
//            }
//
//            if (Context.HttpContext.User.IsInRole(Roles.Admin)) {
//                entity
//                    .WithAction(new ActionBuilder()
//                        .WithName("add-teacher")
//                        .WithTitle("Add Teacher")
//                        .WithMethod("POST")
//                        .WithHref(Url.AbsoluteRouteUrl(Routes.TeacherCreate))
//                        .WithType("application/json")
//                        .WithField(new FieldBuilder()
//                                        .WithName("number")
//                                        .WithType("number"))
//                        .WithField(new FieldBuilder()
//                                        .WithName("name")
//                                        .WithType("text"))
//                        .WithField(new FieldBuilder()
//                                        .WithName("email")
//                                        .WithType("email"))
//                        .WithField(new FieldBuilder()
//                                        .WithName("password")
//                                        .WithType("password")));
//            }
//
//            entity
//                .WithLink(new LinkBuilder()
//                    .WithRel("self")
//                    .WithHref(Url.AbsoluteRouteUrl(Routes.TeacherList)))
//                .WithLink(new LinkBuilder()
//                    .WithRel("index")
//                    .WithHref(Url.AbsoluteRouteUrl(Routes.Index)))
//                .WithNavigationLinks(Url, Routes.TeacherList, items.TotalPages, query);
//
//            return entity.Build();

            return null;
        }

        internal object ClassesCollection(PagedList<Class> items, ListQueryStringDto query)
        {
            //            var entity = new SirenEntityBuilder()
            //                    .WithClass("class")
            //                    .WithClass("collection")
            //                    .WithProperty("totalCount", items.TotalCount)
            //                    .WithProperty("totalPages", items.TotalPages)
            //                    .WithProperty("currentPage", items.CurrentPage)
            //                    .WithProperty("pageSize", items.PageSize);
            //
            //            if (!string.IsNullOrEmpty(query.Search))
            //            {
            //                entity
            //                    .WithProperty("search", query.Search);
            //            }
            //
            //            foreach (var item in items) {
            //                var subEntity = new EmbeddedRepresentationBuilder()
            //                    .WithClass("class")
            //                    .WithProperty("name", item.Name)
            //                    .WithProperty("maxGroupSize", item.MaxGroupSize)
            //                    .WithProperty("autoEnrollment", item.AutoEnrollment)
            //                    .WithLink(
            //                        new LinkBuilder()
            //                            .WithRel("self")
            //                            .WithHref(
            //                                Url.AbsoluteRouteUrl(
            //                                    Routes.ClassEntry,
            //                                    new {id = item.Id})));
            //
            //                var sub = subEntity as EmbeddedRepresentationBuilder;
            //                sub.WithRel("item");
            //
            //                entity.WithSubEntity(sub);
            //                    //TODO: relations
            //            }
            //
            //            //TODO: add actions
            //
            //            entity
            //                .WithLink(new LinkBuilder()
            //                    .WithRel("self")
            //                    .WithHref(Url.AbsoluteRouteUrl(Routes.TeacherClassList)))
            //                .WithLink(new LinkBuilder()
            //                    .WithRel("index")
            //                    .WithHref(Url.AbsoluteRouteUrl(Routes.Index)))
            //                .WithNavigationLinks(Url, Routes.TeacherClassList, items.TotalPages, query);
            //
            //            return entity.Build();
            return null;

        }

        public SirenEntity Entity(Teacher teacher)
        {
            //            var entity = new SirenEntityBuilder()
            //                .WithClass("student")
            //                .WithProperty("number", teacher.Number)
            //                .WithProperty("name", teacher.Name)
            //                .WithProperty("email", teacher.Email)
            //                .WithLink(new LinkBuilder()
            //                    .WithRel("self")
            //                    .WithHref(Url.AbsoluteRouteUrl(Routes.TeacherEntry,
            //                                                    new { number = teacher.Number })))
            //                .WithLink(new LinkBuilder()
            //                    .WithRel(SirenData.REL_TEACHERS_CLASSES)
            //                    .WithHref(Url.AbsoluteRouteUrl(Routes.TeacherClassList,
            //                                                    new { number = teacher.Number })));
            //
            //
            //            if (Context.HttpContext.User.IsInRole(Roles.Admin)) {
            //                entity
            //                    .WithAction(
            //                        new ActionBuilder()
            //                            .WithName("edit-student")
            //                            .WithTitle("Edit Student")
            //                            .WithMethod("PUT")
            //                            .WithHref(Url.AbsoluteRouteUrl(Routes.TeacherEdit,
            //                                                            new { number = teacher.Number }))
            //                            .WithType("application/json")
            //                            .WithField(
            //                                new FieldBuilder()
            //                                    .WithName("name")
            //                                    .WithType("text")
            //                                    .WithValue(teacher.Name))
            //                            .WithField(
            //                                new FieldBuilder()
            //                                    .WithName("email")
            //                                    .WithType("email")
            //                                    .WithValue(teacher.Email)))
            //                    .WithAction(
            //                        new ActionBuilder()
            //                            .WithName("delete-student")
            //                            .WithTitle("Delete Student")
            //                            .WithMethod("DELETE")
            //                            .WithHref(Url.AbsoluteRouteUrl(Routes.TeacherDelete,
            //                                                            new { number = teacher.Number }))
            //                            );
            //            }
            //
            //            return entity.Build();

            return null;
        }
    }
}