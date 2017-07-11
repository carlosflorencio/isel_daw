using System;
using System.Security.Claims;
using API.Models;
using API.Siren;
using FluentSiren.Builders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace API.TransferModels.ResponseModels
{
    public class GroupsSirenHto : SirenCollectionHto<Group>
    {

        protected override string RouteList => Routes.ClassGroupsList;

        protected override string Class => "group";

        public GroupsSirenHto(
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

        protected override SirenEntityBuilder AddEntityActions(SirenEntityBuilder entity, Group item)
        {
            Claim c = Context.HttpContext.User.FindFirst(ClaimTypes.Role);
            if (c != null && c.Value.Equals(Roles.Admin) || c.Value.Equals(Roles.Teacher))
            {
                entity.WithAction(new ActionBuilder()
                        .WithName("delete-group")
                        .WithTitle("Delete Group")
                        .WithMethod("Delete")
                        .WithHref(
                            Url.AbsoluteRouteUrl(
                                Routes.GroupDelete,
                                new { id = item.Id }
                            )
                        ));
            }

            return entity;
        }

        protected override SirenEntityBuilder AddEntityLinks(SirenEntityBuilder entity, Group item)
        {
            return entity
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(Url.AbsoluteRouteUrl(Routes.GroupEntry, item.Id)));
        }

        protected override SirenEntityBuilder AddEntityProperties(SirenEntityBuilder entity, Group item)
        {
            return entity.WithProperty("number", item.Number);
        }

        protected override SirenEntityBuilder AddEntitySubEntities(SirenEntityBuilder entity, Group item)
        {
            if(item.Students != null)
                item.Students.ForEach(i => {
                    entity.WithSubEntity(
                        (SubEntityBuilder)new EmbeddedRepresentationBuilder()
                            .WithRel("item")
                            .WithClass("student")
                            .WithProperty("number", i.Student.Number)
                            .WithProperty("name", i.Student.Name)
                            .WithProperty("email", i.Student.Email)
                    );
                });
                
            return entity;
        }

        /*
        |-----------------------------------------------------------------------
        | Collection
        |-----------------------------------------------------------------------
        */

        protected override SirenEntityBuilder AddCollectionActions(SirenEntityBuilder entity)
        {
            return entity;
        }

        protected override SirenEntityBuilder AddCollectionLinks(SirenEntityBuilder entity)
        {
            return entity
                .WithLink(new LinkBuilder()
                    .WithRel("index")
                    .WithHref(UrlTo(Routes.Index)));
        }
    }
}