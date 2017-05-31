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

    public abstract class SirenHto<T>
    {

        protected readonly IUrlHelper Url;
        protected readonly ActionContext Context;

        protected abstract string Class { get; }

        public SirenHto(
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor)
        {
            Context = actionContextAccessor.ActionContext;
            Url = urlHelperFactory.GetUrlHelper(Context);
        }

        /*
        |-----------------------------------------------------------------------
        | Helpers
        |-----------------------------------------------------------------------
        */
        protected string UrlTo(string route)
        {
            return Url.AbsoluteRouteUrl(route);
        }

    }

}
