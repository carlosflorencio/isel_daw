using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Services;
using API.TransferModels.InputModels;
using API.TransferModels.ResponseModels;
using FluentSiren.Builders;
using FluentSiren.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace API.Siren
{

    public abstract class SirenCollectionHto<T> : SirenEntityHto<T>
    {

        protected abstract string RouteList { get; }

        public SirenCollectionHto(
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor) : base(urlHelperFactory,
            actionContextAccessor) { }

        public SirenEntity Collection(PagedList<T> items, ListQueryStringDto query)
        {
            var entity = new SirenEntityBuilder()
                .WithClass(Class)
                .WithClass("collection");

            AddCollectionProperties(entity, items, query);

            foreach (var item in items)
            {
                var subEntity = SubEntity(item);
                var embeddedEntity = subEntity as EmbeddedRepresentationBuilder;

                if (embeddedEntity != null)
                {
                    embeddedEntity.WithRel("item");

                    entity.WithSubEntity(embeddedEntity);
                }
            }

            AddCollectionActions(entity, items[0]);

            AddNavigationLinks(entity, items.TotalPages, query);

            AddCollectionLinks(entity, items[0]);

            return entity.Build();
        }

        /// <summary>
        /// Add all the Collection Actions
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected abstract SirenEntityBuilder AddCollectionActions(
            SirenEntityBuilder entity,
            T item);

        /// <summary>
        /// Add all the Collection Links
        /// Navigation links are already added (prev, next)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected abstract SirenEntityBuilder AddCollectionLinks(
            SirenEntityBuilder entity, T item);

        /*
        |-----------------------------------------------------------------------
        | Helpers
        |-----------------------------------------------------------------------
        */

        protected SirenEntityBuilder AddCollectionProperties(
            SirenEntityBuilder entity,
            PagedList<T> items,
            ListQueryStringDto query)
        {
            entity
                .WithProperty("totalCount", items.TotalCount)
                .WithProperty("totalPages", items.TotalPages)
                .WithProperty("currentPage", items.CurrentPage)
                .WithProperty("pageSize", items.PageSize);

            if (!string.IsNullOrEmpty(query.Search))
            {
                entity.WithProperty("search", query.Search);
            }

            return entity;
        }

        protected SirenEntityBuilder AddNavigationLinks(
            SirenEntityBuilder entity,
            int totalPages,
            ListQueryStringDto query)
        {
            if (query.Page < totalPages)
            {
                entity
                    .WithLink(new LinkBuilder()
                        .WithRel("next")
                        .WithHref(GenerateLinkToListPage(RouteList, query, 1)));
            }

            if (query.Page > 1)
            {
                entity
                    .WithLink(new LinkBuilder()
                        .WithRel("prev")
                        .WithHref(GenerateLinkToListPage(RouteList, query, -1)));
            }

            return entity;
        }

        private string GenerateLinkToListPage(
            string listRoute,
            ListQueryStringDto query,
            int pageOffset = 0)
        {
            return Url.AbsoluteRouteUrl(listRoute,
                new {
                    page = query.Page + pageOffset,
                    limit = query.Limit,
                    search = query.Search
                });
        }

    }

}
