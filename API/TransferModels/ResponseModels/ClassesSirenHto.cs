using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace API.TransferModels.ResponseModels
{
    public class ClassesSirenHto
    {
        private readonly IUrlHelper _url;
        private readonly ActionContext _context;

        public ClassesSirenHto(IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor)
        {
            _context = actionContextAccessor.ActionContext;
            _url = urlHelperFactory.GetUrlHelper(_context);
        }
    }
}