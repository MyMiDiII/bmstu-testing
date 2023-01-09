using ServerING;
using Integrate;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;


namespace E2E.Utils {
    public class MyWebFactory : WebApplicationFactory<Startup> {
        protected override IHostBuilder CreateHostBuilder() {
            return Host.CreateDefaultBuilder().ConfigureWebHost((builder) =>{
                builder.UseStartup<TestStartup>();
            });
        }
    }
}