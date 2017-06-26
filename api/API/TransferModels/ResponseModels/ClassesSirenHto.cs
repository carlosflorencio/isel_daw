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
                .WithProperty("id", item.Id)
                .WithProperty("name", item.Name)
                .WithProperty("maxGroupSize", item.MaxGroupSize)
                .WithProperty("autoEnrollment", item.AutoEnrollment);
        }

        protected override SirenEntityBuilder AddEntitySubEntities(SirenEntityBuilder entity, Class item)
        {
            return entity
                    .WithSubEntity(
                        (SubEntityBuilder) new EmbeddedRepresentationBuilder()
                         .WithRel("semester")
                         .WithClass("semester")
                         .WithProperty("Id", item.Semester.Id)
                         .WithProperty("Year", item.Semester.Year)
                         .WithProperty("Term", item.Semester.Term)
                    );
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
                            .WithHref(Url.ToClass(Routes.ClassEdit, item.Id))
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
                            .WithHref(Url.ToClass(Routes.CourseDelete, item.Id))
                    );
            }

            return entity;
        }

        protected override SirenEntityBuilder AddEntityLinks(SirenEntityBuilder entity, Class item)
        {
            return entity
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(Url.ToClass(Routes.ClassEntry, item.Id)));
        }

        /*
        |-----------------------------------------------------------------------
        | Collection
        |-----------------------------------------------------------------------
        */

        protected override SirenEntityBuilder AddCollectionActions(
            SirenEntityBuilder entity,
            Class item)
        {
            if (Context.HttpContext.User.IsInRole(Roles.Admin))
            {
                entity
                    .WithAction(
                        new ActionBuilder()
                            .WithName("add-class")
                            .WithTitle("Add Class")
                            .WithMethod("POST")
                            .WithHref(UrlTo(Routes.ClassCreate))
                            .WithType("application/json")
                            .WithField(new FieldBuilder()
                                .WithName("CourseId")
                                .WithType("hidden")
                                .WithValue(item.CourseId.ToString()))
                            .WithField(new FieldBuilder()
                                .WithTitle("Semester")
                                .WithName("SemesterId")
                                .WithType("number"))
                            .WithField(new FieldBuilder()
                                .WithTitle("Name")
                                .WithName("name")
                                .WithType("text"))
                            .WithField(new FieldBuilder()
                                .WithTitle("Group Size")
                                .WithName("maxGroupSize")
                                .WithType("number"))
                            .WithField(new FieldBuilder()
                                .WithTitle("Auto Enrollment")
                                .WithName("autoEnrollment")
                                .WithType("checkbox"))
                    );
            }

            return entity;
        }

        protected override SirenEntityBuilder AddCollectionLinks(
            SirenEntityBuilder entity,
            Class item)
        {
            return entity
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(UrlTo(Routes.CourseClasses)))
                .WithLink(new LinkBuilder()
                    .WithRel("index")
                    .WithHref(UrlTo(Routes.Index)));
        }
    }
}