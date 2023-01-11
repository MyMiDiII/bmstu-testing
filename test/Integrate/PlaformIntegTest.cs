using ServerING.Services;
using ServerING.ModelsBL;
using Integrate.Utils;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Allure.Xunit.Attributes;
using AutoFixture;


namespace Integrate {
    [AllureOwner("EqualNine")]
    [AllureSuite("Platform Integration Tests")]
    public class PlatformIntegTest {
        private PlatformService platformService;

        public PlatformIntegTest() {
            var server = new TestServer(new WebHostBuilder().UseStartup<TestStartup>());
            platformService = (PlatformService)server.Services.GetRequiredService<IPlatformService>();
        }

        [AllureXunit]
        [ResetDatabase]
        public void TestPlatformAdd() {
            var fixture = new Fixture();
            PlatformBL platform = fixture.Create<PlatformBL>();

            PlatformBL result = platformService.AddPlatform(platform);

            Assert.Equal(platform.Name, result.Name);
            Assert.Equal(platform.Cost, result.Cost);
            Assert.Equal(platform.Popularity, result.Popularity);
        }

        [AllureXunitTheory]
        [InlineData(1)]
        [ResetDatabase]
        public void TestPlatformUpdate(int id) {
            var fixture = new Fixture();
            PlatformBL platform = fixture.Create<PlatformBL>();
            platform.Id = id;

            PlatformBL result = platformService.UpdatePlatform(id, platform);

            Assert.Equal(platform.Name, result.Name);
            Assert.Equal(platform.Cost, result.Cost);
            Assert.Equal(platform.Popularity, result.Popularity);
        }

        [AllureXunitTheory]
        [InlineData(1)]
        [ResetDatabase]
        public void TestPlatformDelete(int id) {
            PlatformBL result = platformService.DeletePlatform(id);

            Assert.NotNull(result);
        }

        [AllureXunitTheory]
        [InlineData(1)]
        [ResetDatabase]
        public void TestPlatformGetById(int id) {
            var result = platformService.GetPlatformByID(id);

            Assert.NotNull(result);
        }

        [AllureXunit]
        [ResetDatabase]
        public void TestPlatformGetAll() {
            var result = platformService.GetAllPlatforms();

            Assert.NotNull(result);
        }

        [AllureXunitTheory]
        [InlineData("Platform1")]
        [ResetDatabase]
        public void TestPlatformGetByName(String name) {
            var result = platformService.GetPlatformByName(name);

            Assert.NotNull(result);
        }
    }
}
