using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using API.Siren;
using FluentSiren.Builders;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace API.TransferModels.ResponseModels
{
    public class TestSirenHto : SirenCollectionHto<Teacher>
    {

        public TestSirenHto(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor) : base(urlHelperFactory, actionContextAccessor) { }

        protected override string Class { get; }

        protected override SirenEntityBuilder AddEntityProperties(SirenEntityBuilder entity, Teacher item)
        {
            throw new NotImplementedException();
        }

        protected override SirenEntityBuilder AddEntitySubEntities(SirenEntityBuilder entity, Teacher item)
        {
            throw new NotImplementedException();
        }

        protected override SirenEntityBuilder AddEntityActions(SirenEntityBuilder entity, Teacher item)
        {
            throw new NotImplementedException();
        }

        protected override SirenEntityBuilder AddEntityLinks(SirenEntityBuilder entity, Teacher item)
        {
            throw new NotImplementedException();
        }

        protected override string RouteList { get; }
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
