using System;
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
    public class TeachersSirenHto : SirenCollectionHto<Teacher>
    {
        
        protected override string Class { get; } = "teacher";
        protected override string RouteList { get; } = Routes.TeacherList;

        public TeachersSirenHto(
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

        protected override SirenEntityBuilder AddEntityProperties(SirenEntityBuilder entity, Teacher item)
        {
            return entity
                .WithProperty("number", item.Number)
                .WithProperty("name", item.Name)
                .WithProperty("email", item.Email);
        }

        protected override SirenEntityBuilder AddEntitySubEntities(SirenEntityBuilder entity, Teacher item)
        {
            return entity;
        }

        protected override SirenEntityBuilder AddEntityActions(SirenEntityBuilder entity, Teacher item)
        {
            if (Context.HttpContext.User.IsInRole(Roles.Admin))
            {
                entity
                    .WithAction(
                        new ActionBuilder()
                            .WithName("edit-teacher")
                            .WithTitle("Edit Teacher")
                            .WithMethod("PUT")
                            .WithHref(Url.ToTeacher(Routes.TeacherEdit, item.Number))
                            .WithType("application/json")
                            .WithField(
                                new FieldBuilder()
                                    .WithName("name")
                                    .WithType("text")
                                    .WithValue(item.Name))
                            .WithField(
                                new FieldBuilder()
                                    .WithName("email")
                                    .WithType("email")
                                    .WithValue(item.Email)))
                    .WithAction(
                        new ActionBuilder()
                            .WithName("delete-teacher")
                            .WithTitle("Delete Teacher")
                            .WithMethod("DELETE")
                            .WithHref(Url.ToTeacher(Routes.TeacherDelete, item.Number))
                    );
            }

            return entity;
        }

        protected override SirenEntityBuilder AddEntityLinks(SirenEntityBuilder entity, Teacher item)
        {
            return entity
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(Url.ToTeacher(Routes.TeacherEntry, item.Number)))
                .WithLink(new LinkBuilder()
                    .WithRel(SirenData.REL_TEACHERS_CLASSES)
                    .WithHref(Url.ToTeacher(Routes.TeacherClassList, item.Number)));
        }

        /*
        |-----------------------------------------------------------------------
        | Collection
        |-----------------------------------------------------------------------
        */

        protected override SirenEntityBuilder AddCollectionActions(SirenEntityBuilder entity, Teacher item)
        {
            return entity
                    .WithAction(new ActionBuilder()
                        .WithName("add-teacher")
                        .WithTitle("Add Teacher")
                        .WithMethod("POST")
                        .WithHref(UrlTo(Routes.TeacherCreate))
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
                            .WithType("password"))
                        .WithField(new FieldBuilder()
                            .WithName("isAdmin")
                            .WithType("checkbox")));
        }

        protected override SirenEntityBuilder AddCollectionLinks(SirenEntityBuilder entity, Teacher item)
        {
            return entity
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(UrlTo(Routes.TeacherList)))
                .WithLink(new LinkBuilder()
                    .WithRel("index")
                    .WithHref(UrlTo(Routes.Index)));
        }
    }
}