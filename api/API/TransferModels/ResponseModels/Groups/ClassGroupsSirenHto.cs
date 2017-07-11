using System.Security.Claims;
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
    public class ClassGroupsSirenHto : GroupsSirenHto
    {
        protected override string RouteList => Routes.ClassGroupsList;

        protected int Identifier;

        public ClassGroupsSirenHto(
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor) :
                base(urlHelperFactory, actionContextAccessor)
        {
        }

        public SirenEntity WeakCollection(int parentId, PagedList<Group> items, ListQueryStringDto query)
        {
            Identifier = parentId;
            return this.Collection(items, query);
        }

        protected override SirenEntityBuilder AddCollectionActions(SirenEntityBuilder entity)
        {
            Claim c = Context.HttpContext.User.FindFirst(ClaimTypes.Role);
            if (c != null && (c.Value.Equals(Roles.Admin) || c.Value.Equals(Roles.Teacher)))
            {
                entity
                    .WithAction(new ActionBuilder()
                        .WithName("add-group-to-class")
                        .WithTitle("Add Group to Class")
                        .WithMethod("POST")
                        .WithHref(UrlTo(Routes.GroupCreate))
                        .WithType("application/json")
                            .WithField(new FieldBuilder()
                                .WithName("ClassId")
                                .WithType("hidden")
                                .WithValue(Identifier.ToString()))
                            .WithField(new FieldBuilder()
                                .WithTitle("Number")
                                .WithName("number")
                                .WithType("number"))
                    );
            }

            return entity;
        }

        protected override SirenEntityBuilder AddCollectionLinks(SirenEntityBuilder entity)
        {
            return base.AddCollectionLinks(entity)
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(Url.AbsoluteRouteUrl(
                        RouteList,
                        new { id = Identifier })
                    )
                );
        }
    }
}