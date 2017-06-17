using System;
using System.Collections.Generic;
using API.Extensions;
using API.Models;
using API.Services;
using API.Siren;
using API.TransferModels.InputModels;
using FluentSiren.Builders;
using FluentSiren.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace API.TransferModels.ResponseModels
{
    public class ClassesSirenHto : SirenCollectionHto<Class>
    {
        protected override string Class { get; } = "class";
        protected override string RouteList { get; } = Routes.CourseClasses;

        public ClassesSirenHto(
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor) :
                base(urlHelperFactory, actionContextAccessor)
        {
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

        /*
        |-----------------------------------------------------------------------
        | Entity
        |-----------------------------------------------------------------------
        */

        protected override SirenEntityBuilder AddEntityProperties(SirenEntityBuilder entity, Class item)
        {
            return entity
                .WithProperty("name", item.Name)
                .WithProperty("maxGroupSize", item.MaxGroupSize)
                .WithProperty("autoEnrollment", item.AutoEnrollment);
        }

        protected override SirenEntityBuilder AddEntitySubEntities(SirenEntityBuilder entity, Class item)
        {
            return entity;
        }

        protected override SirenEntityBuilder AddEntityActions(SirenEntityBuilder entity, Class item)
        {
            if (Context.HttpContext.User.IsInRole(Roles.Admin))
            {
                entity
                    .WithAction(
                        new ActionBuilder()
                            .WithName("edit-class")
                            .WithTitle("Edit Class")
                            .WithMethod("PUT")
                            .WithHref(UrlToClass(Routes.ClassEdit, item.Id))
                            .WithType("application/json")
                            .WithField(
                                new FieldBuilder()
                                    .WithName("name")
                                    .WithType("text")
                                    .WithValue(item.Name))
                            .WithField(
                                new FieldBuilder()
                                    .WithName("maxGroupSize")
                                    .WithType("number")
                                    .WithValue(item.MaxGroupSize.ToString()))
                            .WithField(
                                new FieldBuilder()
                                    .WithName("autoEnrollment")
                                    .WithType("checkbox")
                                    .WithValue(item.AutoEnrollment.ToString())))
                    .WithAction(
                        new ActionBuilder()
                            .WithName("delete-class")
                            .WithTitle("Delete Class")
                            .WithMethod("DELETE")
                            .WithHref(UrlToClass(Routes.CourseDelete, item.Id))
                    );
            }

            return entity;
        }

        protected override SirenEntityBuilder AddEntityLinks(SirenEntityBuilder entity, Class item)
        {
            return entity
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(UrlToClass(Routes.ClassEntry, item.Id)));
        }

        /*
        |-----------------------------------------------------------------------
        | Collection
        |-----------------------------------------------------------------------
        */

        protected override SirenEntityBuilder AddCollectionActions(SirenEntityBuilder entity)
        {
            if (Context.HttpContext.User.IsInRole(Roles.Admin))
            {
                entity
                    .WithAction(new ActionBuilder()
                        .WithName("add-class")
                        .WithTitle("Add Class")
                        .WithMethod("POST")
                        .WithHref(UrlTo(Routes.ClassCreate))
                        .WithType("application/json")
                        .WithField(new FieldBuilder()
                            .WithName("name")
                            .WithType("text"))
                        .WithField(new FieldBuilder()
                            .WithName("maxGroupSize")
                            .WithType("number"))
                        .WithField(new FieldBuilder()
                            .WithName("autoEnrollment")
                            .WithType("checkbox")));
            }

            return entity;
        }

        protected override SirenEntityBuilder AddCollectionLinks(SirenEntityBuilder entity)
        {
            return entity
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(UrlTo(Routes.CourseClasses)))
                .WithLink(new LinkBuilder()
                    .WithRel("index")
                    .WithHref(UrlTo(Routes.Index)));
        }

        /*
        |-----------------------------------------------------------------------
        | Helpers
        |-----------------------------------------------------------------------
        */
        private string UrlToClass(string route, int id)
        {
            return Url.AbsoluteRouteUrl(route, new {id = id});
        }
    }
}