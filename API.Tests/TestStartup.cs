using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace API.Tests
{
    public class TestStartup : Startup
    {
        public TestStartup(IHostingEnvironment hostingEnvironment)
            : base(hostingEnvironment)
        {
        }
    }
}
