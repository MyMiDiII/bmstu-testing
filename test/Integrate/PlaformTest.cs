using ServerING.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Integrate {
    public class PlatformTest {
        private PlatformService platformService;

        public PlatformTest() {
            var server = new TestServer(new WebHostBuilder().UseStartup<TestStartup>());
            platformService = (PlatformService)server.Services.GetRequiredService<IPlatformService>();
        }

        [Fact]
        public void GetById() {
            var result = platformService.GetPlatformByID(1);
            Console.WriteLine(result);
            Assert.NotNull(result);
        }
    }
}
