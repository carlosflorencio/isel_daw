using System;
using System.Security.Claims;
using API.Controllers;
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

    public class StudentsSirenHto : SirenCollectionHto<Student>
    {

        protected override string Class { get; } = "student";
        protected override string RouteList { get; } = Routes.StudentList;

        public StudentsSirenHto(
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor) : base(urlHelperFactory,
            actionContextAccessor) { }

        /*
        |-----------------------------------------------------------------------
        | Entity
        |-----------------------------------------------------------------------
        */

        protected override SirenEntityBuilder AddEntityProperties(
            SirenEntityBuilder entity,
            Student item)
            => entity
                .WithProperty("number", item.Number)
                .WithProperty("name", item.Name)
                .WithProperty("email", item.Email);

        protected override SirenEntityBuilder AddEntitySubEntities(
            SirenEntityBuilder entity,
            Student item)
        {
            return entity;
        }

        protected override SirenEntityBuilder AddEntityActions(
            SirenEntityBuilder entity,
            Student item)
        {
            Claim c = Context.HttpContext.User.FindFirst(ClaimTypes.Role);
            if (c != null && c.Value.Equals(Roles.Admin))
            {
                entity
                    .WithAction(
                        new ActionBuilder()
                            .WithName("edit-student")
                            .WithTitle("Edit Student")
                            .WithMethod("PUT")
                            .WithHref(Url.ToStudent(Routes.StudentEdit, item.Number))
                            .WithType("application/json")
                            .WithField(
                                new FieldBuilder()
                                    .WithTitle("Name")
                                    .WithName("name")
                                    .WithType("text")
                                    .WithValue(item.Name))
                            .WithField(
                                new FieldBuilder()
                                    .WithTitle("E-mail")
                                    .WithName("email")
                                    .WithType("email")
                                    .WithValue(item.Email)))
                    .WithAction(
                        new ActionBuilder()
                            .WithName("delete-student")
                            .WithTitle("Delete Student")
                            .WithMethod("DELETE")
                            .WithHref(Url.ToStudent(Routes.StudentDelete, item.Number))
                    );
            }

            return entity;
        }

        protected override SirenEntityBuilder AddEntityLinks(
            SirenEntityBuilder entity,
            Student item)
        {
            return entity
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(Url.ToStudent(Routes.StudentEntry, item.Number)))
                .WithLink(new LinkBuilder()
                    .WithRel(SirenData.REL_STUDENTS_CLASSES)
                    .WithHref(Url.ToStudent(Routes.StudentClassList, item.Number)));
        }

        /*
        |-----------------------------------------------------------------------
        | Collection
        |-----------------------------------------------------------------------
        */

        protected override SirenEntityBuilder AddCollectionActions(
            SirenEntityBuilder entity)
        {
            Claim c = Context.HttpContext.User.FindFirst(ClaimTypes.Role);
            if (c != null && c.Value.Equals(Roles.Admin))
            {
                entity
                    .WithAction(new ActionBuilder()
                        .WithName("add-student")
                        .WithTitle("Add Student")
                        .WithMethod("POST")
                        .WithHref(UrlTo(Routes.StudentCreate))
                        .WithType("application/json")
                        .WithField(new FieldBuilder()
                            .WithTitle("Number")
                            .WithName("number")
                            .WithType("number"))
                        .WithField(new FieldBuilder()
                            .WithTitle("Name")
                            .WithName("name")
                            .WithType("text"))
                        .WithField(new FieldBuilder()
                            .WithTitle("E-mail")
                            .WithName("email")
                            .WithType("email"))
                        .WithField(new FieldBuilder()
                            .WithTitle("Password")
                            .WithName("password")
                            .WithType("password")));
            }

            return entity;
        }

        protected override SirenEntityBuilder AddCollectionLinks(
            SirenEntityBuilder entity)
        {
            return entity
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(UrlTo(Routes.StudentList)))
                .WithLink(new LinkBuilder()
                    .WithRel("index")
                    .WithHref(UrlTo(Routes.Index)));
        }
    }

}
