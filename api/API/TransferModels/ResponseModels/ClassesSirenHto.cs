using System;
using System.Collections.Generic;
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
    public class ClassesSirenHto
    {

        public SirenEntity Entity(Class c) {
            return null;

//            var entity = new SirenEntityBuilder()
//                .WithClass("class")
//                .WithProperty("name", c.Name)
//                .WithProperty("MaxGroupSize", c.MaxGroupSize)
//                .WithLink(new LinkBuilder()
//                    .WithRel("self")
//                    .WithHref(_url.AbsoluteRouteUrl(Routes.ClassEntry,
//                                                    new { id = c.Id })))
//                .WithLink(new LinkBuilder()
//                    .WithRel(SirenData.REL_CLASS_GROUPS)
//                    .WithHref(_url.AbsoluteRouteUrl(Routes.ClassGroupsList,
//                                                    new { id = c.Id })))
//                .WithSubEntity(new EmbeddedRepresentationBuilder()
//                    .WithClass("semester")
//                    .WithRel(SirenData.REL_CLASS_SEMESTER)
//                    .WithProperty("year", c.Semester.Year)
//                    .WithProperty("term", c.Semester.Term)
//                    .WithLink(new LinkBuilder()
//                        .WithRel("self")
//                        .WithHref(_url.AbsoluteRouteUrl(Routes.SemesterEntry,
//                                                    new { id = c.SemesterId }))));
//            //TODO: what to do with course?
//
//            if (_context.HttpContext.User.IsInRole(Roles.Admin)) {
//                entity
//                    .WithAction(
//                        new ActionBuilder()
//                            .WithName("edit-class")
//                            .WithTitle("Edit Class")
//                            .WithMethod("PUT")
//                            .WithHref(_url.AbsoluteRouteUrl(Routes.ClassEdit,
//                                                            new { id = c.Id }))
//                            .WithType("application/json")
//                            .WithField(
//                                new FieldBuilder()
//                                    .WithName("name")
//                                    .WithType("text")
//                                    .WithValue(c.Name))
//                            .WithField(
//                                new FieldBuilder()
//                                    .WithName("autoEnrollment")
//                                    .WithType("checkbox")
//                                    .WithValue(c.AutoEnrollment.ToString()))
//                            .WithField(
//                                new FieldBuilder()
//                                    .WithName("maxGroupSize")
//                                    .WithType("number")
//                                    .WithValue(c.MaxGroupSize.ToString()))
//                            .WithField(
//                                new FieldBuilder()
//                                    .WithName("semesterId")
//                                    .WithType("number")
//                                    .WithValue(c.SemesterId.ToString()))
//                            .WithField(
//                                new FieldBuilder()
//                                    .WithName("courseId")
//                                    .WithType("number")
//                                    .WithValue(c.CourseId.ToString())))
//                    .WithAction(
//                        new ActionBuilder()
//                            .WithName("delete-class")
//                            .WithTitle("Delete Class")
//                            .WithMethod("DELETE")
//                            .WithHref(_url.AbsoluteRouteUrl(Routes.ClassDelete,
//                                                            new { id = c.Id }))
//                            );
//            }
//
//            return entity.Build();
        }

        public SirenEntity Collection(PagedList<Class> items, ListQueryStringDto query)
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
//            foreach (var item in items)
//            {
//                entity
//                    .WithSubEntity(new EmbeddedRepresentationBuilder()
//                    .WithClass("class")
//                    .WithRel("item")
//                    .WithProperty("name", item.Name)
//                    .WithProperty("maxGroupSize", item.MaxGroupSize)
//                    .WithProperty("autoEnrollment", item.AutoEnrollment)
//                    .WithLink(new LinkBuilder()
//                        .WithRel("self")
//                        .WithHref(_url.AbsoluteRouteUrl(Routes.ClassEntry,
//                        new { id = item.Id })))
//                    .WithLink(new LinkBuilder()
//                        .WithRel(SirenData.REL_CLASS_SEMESTER)
//                        .WithHref(_url.AbsoluteRouteUrl(Routes.SemesterEntry,
//                        new { id = item.SemesterId })))
//                    .WithLink(new LinkBuilder()
//                        .WithRel(SirenData.REL_CLASS_GROUPS)
//                        .WithHref(_url.AbsoluteRouteUrl(Routes.ClassGroupsList,
//                        new { id = item.SemesterId }))));
//            }
//
//            if (_context.HttpContext.User.IsInRole(Roles.Admin)) {
//                entity
//                    .WithAction(new ActionBuilder()
//                        .WithName("add-class")
//                        .WithTitle("Add Class")
//                        .WithMethod("POST")
//                        .WithHref(_url.AbsoluteRouteUrl(Routes.ClassCreate))
//                        .WithType("application/json")
//                        .WithField(new FieldBuilder()
//                                        .WithName("name")
//                                        .WithType("text"))
//                        .WithField(new FieldBuilder()
//                                        .WithName("maxGroupSize")
//                                        .WithType("number"))
//                        .WithField(new FieldBuilder()
//                                        .WithName("autoEnrollment")
//                                        .WithType("checkbox"))  //TODO: check
//                        .WithField(new FieldBuilder()
//                                        .WithName("semesterId")
//                                        .WithType("number"))
//                        .WithField(new FieldBuilder()
//                                        .WithName("courseId")
//                                        .WithType("number"))
//                        );
//            }
//
//            entity
//                .WithLink(new LinkBuilder()
//                    .WithRel("self")
//                    .WithHref(_url.AbsoluteRouteUrl(Routes.ClassList)))
//                .WithLink(new LinkBuilder()
//                    .WithRel("index")
//                    .WithHref(_url.AbsoluteRouteUrl(Routes.Index)))
//                .WithNavigationLinks(_url, Routes.ClassList, items.TotalPages, query);
//
//            return entity.Build();

            return null;
        }

        public SirenEntity ClassGroupsCollection(PagedList<Group> groups, ListQueryStringDto query)
        {
//            var entity = new SirenEntityBuilder()
//                    .WithClass("group")
//                    .WithClass("collection")
//                    .WithProperty("totalCount", groups.TotalCount)
//                    .WithProperty("totalPages", groups.TotalPages)
//                    .WithProperty("currentPage", groups.CurrentPage)
//                    .WithProperty("pageSize", groups.PageSize);
//
//            if (!string.IsNullOrEmpty(query.Search))
//            {
//                entity
//                    .WithProperty("search", query.Search);
//            }
//
//            foreach (var group in groups)
//            {
//                entity
//                    .WithSubEntity(new EmbeddedRepresentationBuilder()
//                    .WithClass("group")
//                    .WithRel("item")
//                    .WithProperty("number", group.Id)   //TODO: change this
//                    .WithLink(new LinkBuilder()
//                        .WithRel("self")
//                        .WithHref(_url.AbsoluteRouteUrl(Routes.GroupEntry,
//                        new { id = group.Id })))
//                    .WithLink(new LinkBuilder()
//                        .WithRel(SirenData.REL_GROUP_CLASS)
//                        .WithHref(_url.AbsoluteRouteUrl(Routes.ClassEntry,
//                        new { id = group.ClassId })))
//                    .WithLink(new LinkBuilder()
//                        .WithRel(SirenData.REL_GROUP_STUDENTS)
//                        .WithHref(_url.AbsoluteRouteUrl(Routes.GroupStudentsList,
//                        new { id = group.Id }))));
//            }
//
//            if (_context.HttpContext.User.IsInRole(Roles.Admin)) {
//                entity
//                    .WithAction(new ActionBuilder()
//                        .WithName("add-class")
//                        .WithTitle("Add Class")
//                        .WithMethod("POST")
//                        .WithHref(_url.AbsoluteRouteUrl(Routes.ClassCreate))
//                        .WithType("application/json")
//                        .WithField(new FieldBuilder()
//                                        .WithName("name")
//                                        .WithType("text"))
//                        .WithField(new FieldBuilder()
//                                        .WithName("maxGroupSize")
//                                        .WithType("number"))
//                        .WithField(new FieldBuilder()
//                                        .WithName("autoEnrollment")
//                                        .WithType("checkbox"))
//                        .WithField(new FieldBuilder()
//                                        .WithName("semesterId")
//                                        .WithType("number"))
//                        .WithField(new FieldBuilder()
//                                        .WithName("courseId")
//                                        .WithType("number")));
//            }
//
//            entity
//                .WithLink(new LinkBuilder()
//                    .WithRel("self")
//                    .WithHref(_url.AbsoluteRouteUrl(Routes.ClassGroupsList)))
//                .WithLink(new LinkBuilder()
//                    .WithRel("index")
//                    .WithHref(_url.AbsoluteRouteUrl(Routes.Index)))
//                .WithNavigationLinks(_url, Routes.ClassGroupsList, groups.TotalPages, query);
//
//            return entity.Build();

            return null;
        }
    }
}