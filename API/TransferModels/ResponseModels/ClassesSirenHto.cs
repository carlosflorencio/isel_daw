using System;
using System.Collections.Generic;
using API.Models;
using API.TransferModels.InputModels;
using FluentSiren.Builders;
using FluentSiren.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace API.TransferModels.ResponseModels
{
    public class ClassesSirenHto : SirenHto
    {
        public ClassesSirenHto(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor) : base(urlHelperFactory, actionContextAccessor)
        {
        }

        public Entity Entity(Class c)
        {
            var entity = new EntityBuilder()
                .WithClass("class")
                .WithProperty("name", c.Name)
                .WithProperty("MaxGroupSize", c.MaxGroupSize)
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(_url.AbsoluteRouteUrl(Routes.ClassEntry,
                                                    new { id = c.Id })))
                .WithSubEntity(new EmbeddedRepresentationBuilder()
                    .WithClass("semester")
                    .WithRel(SirenData.REL_CLASS_SEMESTER)
                    .WithProperty("year", c.Semester.Year)
                    .WithProperty("term", c.Semester.Term)
                    .WithLink(new LinkBuilder()
                        .WithRel("self")
                        .WithHref(_url.AbsoluteRouteUrl(Routes.SemesterEntry,
                                                    new { id = c.Semester.Id }))));
            //TODO: what to do with course?
            
            if (_context.HttpContext.User.IsInRole(Roles.Admin)) {
                entity
                    .WithAction(
                        new ActionBuilder()
                            .WithName("edit-class")
                            .WithTitle("Edit Class")
                            .WithMethod("PUT")
                            .WithHref(_url.AbsoluteRouteUrl(Routes.ClassEdit,
                                                            new { id = c.Id }))
                            .WithType("application/json")
                            .WithField(
                                new FieldBuilder()
                                    .WithName("name")
                                    .WithType("text")
                                    .WithValue(c.Name))
                            .WithField(
                                new FieldBuilder()
                                    .WithName("autoEnrollment")
                                    .WithType("bool")
                                    .WithValue(c.AutoEnrollment.ToString()))                                    
                            .WithField(
                                new FieldBuilder()
                                    .WithName("maxGroupSize")
                                    .WithType("number")
                                    .WithValue(c.MaxGroupSize.ToString()))
                            .WithField(
                                new FieldBuilder()
                                    .WithName("semesterId")
                                    .WithType("number")
                                    .WithValue(c.SemesterId.ToString()))
                            .WithField(
                                new FieldBuilder()
                                    .WithName("courseId")
                                    .WithType("number")
                                    .WithValue(c.CourseId.ToString())))
                    .WithAction(
                        new ActionBuilder()
                            .WithName("delete-class")
                            .WithTitle("Delete Class")
                            .WithMethod("DELETE")
                            .WithHref(_url.AbsoluteRouteUrl(Routes.ClassDelete,
                                                            new { id = c.Id }))
                            );
            }

            return entity.Build();
        }

        internal object Collection(List<Class> classes, ListQueryStringDto query)
        {
            var entity = new EntityBuilder()
                .WithClass("class");

            if (_context.HttpContext.User.IsInRole(Roles.Admin)) {
                entity
                    .WithAction(new ActionBuilder()
                        .WithName("add-class")
                        .WithTitle("Add Class")
                        .WithMethod("POST")
                        .WithHref(_url.AbsoluteRouteUrl(Routes.ClassCreate))
                        .WithType("application/json")
                        .WithField(new FieldBuilder()
                                        .WithName("name")
                                        .WithType("text"))
                        .WithField(new FieldBuilder()
                                        .WithName("maxGroupSize")
                                        .WithType("number"))
                        .WithField(new FieldBuilder()
                                        .WithName("autoEnrollment")
                                        .WithType("bool"))  //TODO: check
                        .WithField(new FieldBuilder()
                                        .WithName("semesterId")
                                        .WithType("number"))
                        .WithField(new FieldBuilder()
                                        .WithName("courseId")
                                        .WithType("number"))
                        );
            }
            //TODO: implement
            throw new NotImplementedException();
        }
    }
}