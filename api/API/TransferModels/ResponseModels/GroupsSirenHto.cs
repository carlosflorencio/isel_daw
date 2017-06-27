using System;
using API.Models;
using API.Siren;
using FluentSiren.Builders;
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
            return entity;
        }

        protected override SirenEntityBuilder AddEntityLinks(SirenEntityBuilder entity, Group item)
        {
            return entity
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(Url.ToGroup(Routes.GroupEntry, item.Id, item.ClassId)));
        }

        protected override SirenEntityBuilder AddEntityProperties(SirenEntityBuilder entity, Group item)
        {
            return entity.WithProperty("number", item.Id);
        }

        protected override SirenEntityBuilder AddEntitySubEntities(SirenEntityBuilder entity, Group item)
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
            return entity;
        }

        protected override SirenEntityBuilder AddCollectionLinks(SirenEntityBuilder entity)
        {
            return entity
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(UrlTo(Routes.ClassGroupsList)))   //TODO: missing class id
                .WithLink(new LinkBuilder()
                    .WithRel("index")
                    .WithHref(UrlTo(Routes.Index)));
        }
    }
}