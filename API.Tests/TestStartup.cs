using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyTested.AspNetCore.Mvc;

namespace API.Tests
{
    public class TestStartup : Startup
    {
        public TestStartup(IHostingEnvironment hostingEnvironment)
            : base(hostingEnvironment)
        {
        }

//        public void ConfigureTestServices(IServiceCollection services)
//        {
//            base.ConfigureServices(services);
//
//            services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase());
//        }
    }
}
