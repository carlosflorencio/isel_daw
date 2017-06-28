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

        protected override string RouteList => Routes.ClassList;

        public ClassesSirenHto(
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor) :
                base(urlHelperFactory, actionContextAccessor)
        {
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
            return entity.WithSubEntity(
                (SubEntityBuilder)new EmbeddedRepresentationBuilder()
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
                                    .WithTitle("Name")
                                    .WithName("name")
                                    .WithType("text")
                                    .WithValue(item.Name))
                            .WithField(
                                new FieldBuilder()
                                    .WithTitle("Max Group Size")
                                    .WithName("maxGroupSize")
                                    .WithType("number")
                                    .WithValue(item.MaxGroupSize.ToString()))
                            .WithField(
                                new FieldBuilder()
                                    .WithTitle("Auto Enrollment")
                                    .WithName("autoEnrollment")
                                    .WithType("checkbox")
                                    .WithValue(item.AutoEnrollment.ToString())))
                    .WithAction(
                        new ActionBuilder()
                            .WithName("delete-class")
                            .WithTitle("Delete Class")
                            .WithMethod("DELETE")
                            .WithHref(Url.ToClass(Routes.CourseDelete, item.Id)))
                    .WithAction(
                        new ActionBuilder()
                            .WithName("add-teacher-to-class")
                            .WithTitle("Add Teacher to Class")
                            .WithMethod("POST")
                            .WithHref(
                                Url.AbsoluteRouteUrl(
                                    Routes.ClassTeacherAdd, new {id = item.Id}
                                )
                            )
                            .WithType("application/json")
                            .WithField(new FieldBuilder()
                                .WithTitle("Number")
                                .WithName("number")
                                .WithType("text")));
                    // .WithAction(new ActionBuilder()
                    //         .WithName("remove-teacher-from-class")
                    //         .WithTitle("Remove Teacher from Class")
                    //         .WithMethod("DELETE")
                    //         .WithHref(
                    //             Url.AbsoluteRouteUrl(
                    //                 Routes.ClassTeacherRemove, new {id = item.Id, teacherId = }
                    //             )
                    //         )
                    //         .WithType("application/json")
                    //         .WithField(new FieldBuilder()
                    //             .WithTitle("Number")
                    //             .WithName("number")
                    //             .WithType("text"))
                    // ); 
            }

            return entity;
        }

        protected override SirenEntityBuilder AddEntityLinks(SirenEntityBuilder entity, Class item) {
            return entity
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(Url.ToClass(Routes.ClassEntry, item.Id)))
                .WithLink(new LinkBuilder()
                    .WithRel(SirenData.REL_CLASS_TEACHERS)
                    .WithHref(Url.AbsoluteRouteUrl(
                        Routes.ClassTeachersList, new { id = item.Id })))
                .WithLink(new LinkBuilder()
                    .WithRel(SirenData.REL_CLASS_STUDENTS)
                    .WithHref(Url.AbsoluteRouteUrl(
                        Routes.ClassStudentsList, new {id = item.Id})))
                .WithLink(new LinkBuilder()
                    .WithRel(SirenData.REL_CLASS_GROUPS)
                    .WithHref(Url.AbsoluteRouteUrl(
                        Routes.ClassGroupsList, new {id = item.Id}))
                );
        }

        /*
        |-----------------------------------------------------------------------
        | Collection
        |-----------------------------------------------------------------------
        */

        protected override SirenEntityBuilder AddCollectionActions(
            SirenEntityBuilder entity)
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
                                .WithTitle("Course")
                                .WithName("CourseId")
                                .WithType("number"))
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
            SirenEntityBuilder entity)
        {
            return entity
                .WithLink(new LinkBuilder()
                    .WithRel("index")
                    .WithHref(UrlTo(Routes.Index)))
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(UrlTo(RouteList)));
        }
    }
}