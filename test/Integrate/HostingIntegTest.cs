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
    [AllureSuite("Hosting Integration Tests")]
    public class HostingIntegTest {
        private HostingService hostingService;

        public HostingIntegTest() {
            var server = new TestServer(new WebHostBuilder().UseStartup<TestStartup>());
            hostingService = (HostingService)server.Services.GetRequiredService<IHostingService>();
        }

        [AllureXunit]
        [ResetDatabase]
        public void TestHostingAdd() {
            var fixture = new Fixture();
            WebHostingBL hosting = fixture.Create<WebHostingBL>();

            WebHostingBL result = hostingService.AddHosting(hosting);

            Assert.Equal(hosting.Name, result.Name);
            Assert.Equal(hosting.SubMonths, result.SubMonths);
            Assert.Equal(hosting.PricePerMonth, result.PricePerMonth);
        }

        [AllureXunitTheory]
        [InlineData(1)]
        [ResetDatabase]
        public void TestHostingUpdate(int id) {
            var fixture = new Fixture();
            WebHostingBL hosting = fixture.Create<WebHostingBL>();
            hosting.Id = id;

            WebHostingBL result = hostingService.UpdateHosting(id, hosting);

            Assert.Equal(hosting.Name, result.Name);
            Assert.Equal(hosting.SubMonths, result.SubMonths);
            Assert.Equal(hosting.PricePerMonth, result.PricePerMonth);
        }

        [AllureXunitTheory]
        [InlineData(1)]
        [ResetDatabase]
        public void TestHostingDelete(int id) {
            WebHostingBL result = hostingService.DeleteHosting(id);

            Assert.NotNull(result);
        }

        [AllureXunitTheory]
        [InlineData(1)]
        [ResetDatabase]
        public void TestHostingGetById(int id) {
            var result = hostingService.GetHostingByID(id);

            Assert.Null(result);
        }

        [AllureXunit]
        [ResetDatabase]
        public void TestHostingGetAll() {
            var result = hostingService.GetAllHostings();

            Assert.NotNull(result);
        }

        [AllureXunitTheory]
        [InlineData("Hosting1")]
        [ResetDatabase]
        public void TestHostingGetByName(String name) {
            var result = hostingService.GetHostingByName(name);

            Assert.NotNull(result);
        }
    }
}
