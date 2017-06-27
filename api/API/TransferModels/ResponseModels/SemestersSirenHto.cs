
using System;
using API.Models;
using API.Siren;
using FluentSiren.Builders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace API.TransferModels.ResponseModels
{
    public class SemestersSirenHto : SirenCollectionHto<Semester>
    {

        protected override string RouteList => null;

        protected override string Class => "semester";

        public SemestersSirenHto(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor) : base(urlHelperFactory, actionContextAccessor)
        {
        }

        /*
        |-----------------------------------------------------------------------
        | Entity
        |-----------------------------------------------------------------------
        */

        protected override SirenEntityBuilder AddEntityActions(SirenEntityBuilder entity, Semester item)
        {
            return entity;
        }

        protected override SirenEntityBuilder AddEntityLinks(SirenEntityBuilder entity, Semester item)
        {
            return entity
                .WithLink(new LinkBuilder()
                    .WithRel("self")
                    .WithHref(Url.AbsoluteRouteUrl(Routes.SemesterEntry, new {Id = item.Id})));
        }

        protected override SirenEntityBuilder AddEntityProperties(SirenEntityBuilder entity, Semester item)
        {
            return entity
                .WithProperty("Id", item.Id)
                .WithProperty("Year", item.Year)
                .WithProperty("Term", item.Term);
        }

        protected override SirenEntityBuilder AddEntitySubEntities(SirenEntityBuilder entity, Semester item)
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
            throw new NotImplementedException();
        }

        protected override SirenEntityBuilder AddCollectionLinks(SirenEntityBuilder entity)
        {
            throw new NotImplementedException();
        }
    }
}