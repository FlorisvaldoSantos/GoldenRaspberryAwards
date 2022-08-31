using GoldenRaspberryAwards;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace Aplication.IntegrationTests
{
    public class CatalogApi : WebApplicationFactory<global::GoldenRaspberryAwards.Program>
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            var builder = Host.CreateDefaultBuilder()
                              .ConfigureWebHostDefaults(x =>
                              {
                                  x.UseStartup<Startup>().UseTestServer();
                              });
            return builder;
        }
    }

    
}
