using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.TransferModels.ResponseModels;
using FluentSiren.Builders;
using FluentSiren.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace API.Siren
{

    public abstract class SirenEntityHto<T> : SirenHto<T>
    {

        public SirenEntityHto(
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor) : base(urlHelperFactory, actionContextAccessor) { }

        /// <summary>
        /// Add all the item Properties
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        protected abstract SirenEntityBuilder AddEntityProperties(SirenEntityBuilder entity, T item);

        /// <summary>
        /// Add all the item Sub Entities
        /// Can be a Link Representation or Embedded Representation
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        protected abstract SirenEntityBuilder AddEntitySubEntities(SirenEntityBuilder entity, T item);

        /// <summary>
        /// Add all the Actions of the entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        protected abstract SirenEntityBuilder AddEntityActions(SirenEntityBuilder entity, T item);

        /// <summary>
        /// Add all the entity Links
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        protected abstract SirenEntityBuilder AddEntityLinks(SirenEntityBuilder entity, T item);

        public SirenEntity Entity(T item)
        {
            return SubEntity(item).Build();
        }

        protected SirenEntityBuilder SubEntity(T item)
        {
            var entity = new EmbeddedRepresentationBuilder()
                .WithClass(Class);

            AddEntityProperties(entity, item);

            AddEntitySubEntities(entity, item);

            AddEntityActions(entity, item);

            AddEntityLinks(entity, item);

            return entity;
        }

    }

}
