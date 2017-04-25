using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace API.TransferModels.ResponseModels
{
    public abstract class SirenHto
    {
        protected readonly IUrlHelper _url;
        protected readonly ActionContext _context;

        public SirenHto(IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor)
        {
            _context = actionContextAccessor.ActionContext;
            _url = urlHelperFactory.GetUrlHelper(_context);
        }
    }
}