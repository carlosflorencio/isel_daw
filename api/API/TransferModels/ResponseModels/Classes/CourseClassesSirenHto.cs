using System;
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
    public class CourseClassesSirenHto : ClassesSirenHto
    {
        protected override string RouteList => Routes.CourseClassList;

        protected int Identifier;

        public CourseClassesSirenHto(
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor)
                : base(urlHelperFactory, actionContextAccessor)
        {
        }

        public SirenEntity WeakCollection(int Id, PagedList<Class> items, ListQueryStringDto query)
        {
            Identifier = Id;
            return this.Collection(items, query);
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
                    .WithHref(
                        Url.AbsoluteRouteUrl(
                            RouteList, 
                            new { id = Identifier }
                        )
                    ));
        }

    }
}