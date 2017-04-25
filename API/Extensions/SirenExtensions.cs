using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.TransferModels.InputModels;
using FluentSiren.Builders;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions
{
    public static class SirenExtensions
    {

        public static EntityBuilder WithNavigationLinks(this EntityBuilder entity, IUrlHelper url, string route, int totalPages, ListQueryStringDto query)
        {

            if (query.Page < totalPages) {
                entity
                    .WithLink(new LinkBuilder()
                        .WithRel("next")
                        .WithHref(GenerateLinkToListPage(url, route, query, 1)));
            }

            if (query.Page > 1) {
                entity
                    .WithLink(new LinkBuilder()
                    .WithRel("prev")
                    .WithHref(GenerateLinkToListPage(url, route, query, -1)));
            }

            return entity;
        }

        private static string GenerateLinkToListPage(IUrlHelper url, string listRoute, ListQueryStringDto query, int pageOffset = 0)
        {
            return url.AbsoluteRouteUrl(listRoute, new
            {
                page = query.Page + pageOffset,
                limit = query.Limit,
                search = query.Search
            });
        }
    }
}
