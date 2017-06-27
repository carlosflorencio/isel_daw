using System;
using API.Models;
using API.Siren;
using FluentSiren.Builders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace API.TransferModels.ResponseModels
{
    public class CoursesSirenHto : SirenCollectionHto<Course>
    {
        protected override string Class { get; } = "course";
        protected override string RouteList { get; } = Routes.CourseList;

        public CoursesSirenHto(
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

        protected override SirenEntityBuilder AddEntityActions(SirenEntityBuilder entity, Course item)
        {
            if (Context.HttpContext.User.IsInRole(Roles.Admin))
            {
                entity
                    .WithAction(
                        new ActionBuilder()
                            .WithName("edit-course")
                            .WithTitle("Edit Course")
                            .WithMethod("PUT")
                            .WithHref(Url.ToCourse(Routes.CourseEdit, item.Id))
                            .WithType("application/json")
                            .WithField(
                                new FieldBuilder()
                                    .WithTitle("Name")
                                    .WithName("name")
                                    .WithType("text")
                                    .WithValue(item.Name))
                            .WithField(
                                new FieldBuilder()
                                    .WithTitle("Acronym")
                                    .WithName("acronym")
                                    .WithType("text")
                                    .WithValue(item.Acronym))
                            .WithField(
                                new FieldBuilder()
                                    .WithTitle("Coordinator Number")
                                    .WithName("coordinatorId")
                                    .WithType("number")
                                    .WithValue(item.CoordinatorId.ToString()))
                    )
                    .WithAction(
                        new ActionBuilder()
                            .WithName("add-class-to-course")
                            .WithTitle("Add Class to Course")
                            .WithMethod("POST")
                            .WithHref(UrlTo(Routes.ClassCreate))
                            .WithType("application/json")
                            .WithField(new FieldBuilder()
                                .WithName("CourseId")
                                .WithType("hidden")
                                .WithValue(item.Id.ToString()))
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
                                .WithType("checkbox")))
                    .WithAction(
                        new ActionBuilder()
                            .WithName("delete-course")
                            .WithTitle("Delete Course")
                            .WithMethod("DELETE")
                            .WithHref(Url.ToCourse(Routes.CourseDelete, item.Id))
                    );
            }

            return entity;
        }

        protected override SirenEntityBuilder AddEntityLinks(SirenEntityBuilder entity, Course item)
        {
            return entity
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(Url.ToCourse(Routes.CourseEntry, item.Id)))
                .WithLink(new LinkBuilder()
                    .WithRel(SirenData.REL_COURSE_CLASSES)
                    .WithHref(Url.ToCourse(Routes.CourseClasses, item.Id)))
                .WithLink(new LinkBuilder()
                    .WithRel(SirenData.REl_COURSE_COORDINATOR)
                    .WithHref(Url.ToTeacher(Routes.TeacherEntry, item.CoordinatorId)));
        }

        protected override SirenEntityBuilder AddEntityProperties(SirenEntityBuilder entity, Course item)
        {
            return entity
                .WithProperty("id", item.Id)
                .WithProperty("name", item.Name)
                .WithProperty("acr", item.Acronym);
        }

        protected override SirenEntityBuilder AddEntitySubEntities(SirenEntityBuilder entity, Course item)
        {
            return entity;
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
                        .WithName("add-course")
                        .WithTitle("Add Course")
                        .WithMethod("POST")
                        .WithHref(UrlTo(Routes.CourseCreate))
                        .WithType("application/json")
                        .WithField(new FieldBuilder()
                            .WithTitle("Name")
                            .WithName("name")
                            .WithType("text"))
                        .WithField(new FieldBuilder()
                            .WithTitle("Acronym")
                            .WithName("acronym")
                            .WithType("text"))
                        .WithField(new FieldBuilder()
                            .WithTitle("Coordinator Number")
                            .WithName("coordinatorId")
                            .WithType("number")));
            }

            return entity;
        }

        protected override SirenEntityBuilder AddCollectionLinks(SirenEntityBuilder entity)
        {
            return entity
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(UrlTo(Routes.CourseList)))
                .WithLink(new LinkBuilder()
                    .WithRel("index")
                    .WithHref(UrlTo(Routes.Index)));
        }
    }
}