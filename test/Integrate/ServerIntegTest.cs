using ServerING.Services;
using ServerING.ModelsBL;
using ServerING.Enums;
using ServerING.DTO;
using ServerING.Models;
using Integrate.Utils;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Allure.Xunit.Attributes;

namespace Integrate {
    [AllureOwner("EqualNine")]
    [AllureSuite("Server Integration Tests")]
    public class ServerIntegTest {
        private ServerService serverService;

        public ServerIntegTest() {
            var server = new TestServer(new WebHostBuilder().UseStartup<TestStartup>());
            serverService = (ServerService)server.Services.GetRequiredService<IServerService>();
        }

        [AllureXunit]
        [ResetDatabase]
        public void TestServerAdd() {
            ServerBL server = new ServerBL {
                Name = "ServerTest",
                Ip = "0.0.0.0",
                GameName = "GameTest",
                Status = ServerStatus.Accepted,
                HostingID = 1,
                PlatformID = 1,
                CountryID = 1,
                OwnerID = 1,
            };

            ServerBL result = serverService.AddServer(server);

            Assert.Equal(server.Name, result.Name);
            Assert.Equal(server.Ip, result.Ip);
            Assert.Equal(server.GameName, result.GameName);
            Assert.Equal(server.Status, result.Status);
            Assert.Equal(server.HostingID, result.HostingID);
            Assert.Equal(server.PlatformID, result.PlatformID);
            Assert.Equal(server.CountryID, result.CountryID);
            Assert.Equal(server.OwnerID, result.OwnerID);
        }

        [AllureXunitTheory]
        [InlineData(1)]
        [ResetDatabase]
        public void TestServerUpdate(int id) {
            ServerBL server = new ServerBL {
                Id = 1,
                Name = "ServerTest",
                Ip = "0.0.0.0",
                GameName = "GameTest",
                Status = ServerStatus.Accepted,
                HostingID = 1,
                PlatformID = 1,
                CountryID = 1,
                OwnerID = 1,
            };

            ServerBL result = serverService.UpdateServer(server);

            Assert.Equal(server.Name, result.Name);
            Assert.Equal(server.Ip, result.Ip);
            Assert.Equal(server.GameName, result.GameName);
            Assert.Equal(server.Status, result.Status);
            Assert.Equal(server.HostingID, result.HostingID);
            Assert.Equal(server.PlatformID, result.PlatformID);
            Assert.Equal(server.CountryID, result.CountryID);
            Assert.Equal(server.OwnerID, result.OwnerID);
        }

        [AllureXunitTheory]
        [InlineData(1)]
        [ResetDatabase]
        public void TestServerDelete(int id) {
            ServerBL result = serverService.DeleteServer(id);

            Assert.NotNull(result);
        }

        [AllureXunitTheory]
        [InlineData(1)]
        [ResetDatabase]
        public void TestServerGetById(int id) {
            var result = serverService.GetServerByID(id);

            Assert.NotNull(result);
        }

        [AllureXunit]
        [ResetDatabase]
        public void TestServerGetAll() {
            var result = serverService.GetAllServers(new ServerFilterDto(), null, null, null);

            Assert.NotNull(result);
        }

        [AllureXunitTheory]
        [InlineData("Server1")]
        [ResetDatabase]
        public void TestServerGetByName(String name) {
            var result = serverService.GetServerByName(name);

            Assert.NotNull(result);
        }

        [AllureXunit]
        [ResetDatabase]
        public void TestServerFilter() {
            var servers = serverService
                .GetAllServers(new ServerFilterDto(), null, null, null);
            var filter = new ServerFilterDto() { Name = "1" };

            var result = serverService
                .ProcessServers(servers, filter, null, null, null);

            Assert.NotNull(result);
        }

        [AllureXunit]
        [ResetDatabase]
        public void TestServerSort() {
            var filter = new ServerFilterDto();
            var servers = serverService
                .GetAllServers(filter, null, null, null);

            var result = serverService
                .ProcessServers(servers, filter, ServerSortState.NameDesc, null, null);

            Assert.NotNull(result);
        }

        [AllureXunit]
        [ResetDatabase]
        public void TestServerPagination() {
            var filter = new ServerFilterDto();
            var servers = serverService
                .GetAllServers(filter, null, null, null);

            var result = serverService
                .ProcessServers(servers, filter, null, page: 1, pageSize: 1);

            Assert.NotNull(result);
        }

        [AllureXunitTheory]
        [InlineData(1)]
        [ResetDatabase]
        public void TestServerGetServerPlayers(int id) {
            var result = serverService.GetServerPlayers(id);

            Assert.NotNull(result);
        }

        [AllureXunitTheory]
        [InlineData(1, +1)]
        [ResetDatabase]
        public void TestServerUpdateServerRating(int id, int change) {
            serverService.UpdateServerRating(id, change);
            var changedServer = serverService.GetServerByID(id);

            Assert.Equal(2, changedServer.Rating);
        }
    }
}
