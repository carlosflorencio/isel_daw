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
