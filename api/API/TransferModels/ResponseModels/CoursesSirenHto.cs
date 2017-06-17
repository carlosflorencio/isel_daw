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
                            .WithHref(UrlToCourse(Routes.CourseEdit, item.Id))
                            .WithType("application/json")
                            .WithField(
                                new FieldBuilder()
                                    .WithName("name")
                                    .WithType("text")
                                    .WithValue(item.Name))
                            .WithField(
                                new FieldBuilder()
                                    .WithName("acronym")
                                    .WithType("text")
                                    .WithValue(item.Acronym))
                            .WithField(
                                new FieldBuilder()
                                    .WithName("coordinatorId")
                                    .WithType("number")
                                    .WithValue(item.CoordinatorId.ToString())))
                    .WithAction(
                        new ActionBuilder()
                            .WithName("delete-course")
                            .WithTitle("Delete Course")
                            .WithMethod("DELETE")
                            .WithHref(UrlToCourse(Routes.CourseDelete, item.Id))
                    );
            }

            return entity;
        }

        protected override SirenEntityBuilder AddEntityLinks(SirenEntityBuilder entity, Course item)
        {
            return entity
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(UrlToCourse(Routes.CourseEntry, item.Id)));
        }

        protected override SirenEntityBuilder AddEntityProperties(SirenEntityBuilder entity, Course item)
        {
            return entity
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
                            .WithName("name")
                            .WithType("text"))
                        .WithField(new FieldBuilder()
                            .WithName("acronym")
                            .WithType("text"))
                        .WithField(new FieldBuilder()
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

        /*
        |-----------------------------------------------------------------------
        | Helpers
        |-----------------------------------------------------------------------
        */
        private string UrlToCourse(string route, int id)
        {
            return Url.AbsoluteRouteUrl(route, new {id = id});
        }
    }
}