using AutoMapper;
using Microsoft.EntityFrameworkCore;

using ServerING.Models;
using ServerING.Repository;
using ServerING.Utils;


namespace UnitDB {
    public class ServerRepositoryTest {
        private IMapper mapper;

        public ServerRepositoryTest () {
            var mockMapper = new MapperConfiguration(cfg => {
                cfg.AddProfile(new AutoMappingProfile());
            });
            mapper = mockMapper.CreateMapper();
        }

        private void LoadFixtures(TestData data, AppDBContent context, bool loadServer = true) {
            context.Country.AddRange(data.country);
            context.Platform.AddRange(data.platform);
            context.WebHosting.AddRange(data.hosting);
            context.User.AddRange(data.user);
            context.Player.AddRange(data.player);
            context.ServerPlayer.AddRange(data.serverPlayer);

            if (loadServer)
                context.Server.AddRange(data.server);

            context.SaveChanges();
        }

        [Fact]
        public void TestServerAdd() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context, loadServer: false);

                ServerRepository serverRepository = new ServerRepository(context, mapper);
                var actualServer = serverRepository.Add(data.server);

                Assert.Equal(data.server.Name, actualServer.Name);
                Assert.Equal(data.server.Ip, actualServer.Ip);
                Assert.Equal(data.server.GameName, actualServer.GameName);
                Assert.Equal(data.server.Status, actualServer.Status);
                Assert.Equal(data.server.HostingID, actualServer.HostingID);
                Assert.Equal(data.server.PlatformID, actualServer.PlatformID);
                Assert.Equal(data.server.CountryID, actualServer.CountryID);
                Assert.Equal(data.server.OwnerID, actualServer.OwnerID);

                var isServerWithSuchIP = context.Server.Where(s => s.Ip == data.server.Ip).Any();
                Assert.True(isServerWithSuchIP);
            }
        }

        [Fact]
        public void TestServerUpdate() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);
                var newIp = "1.1.1.1";

                Server updatedServer = new Server {
                    Id = 1,
                    Name = data.server.Name,
                    Ip = newIp,
                    GameName = data.server.GameName,
                    Status = data.server.Status,
                    HostingID = data.server.HostingID,
                    PlatformID = data.server.PlatformID,
                    CountryID = data.server.CountryID,
                    OwnerID = data.server.OwnerID,
                };

                ServerRepository serverRepository = new ServerRepository(context, mapper);
                var actualServer = serverRepository.Update(updatedServer);

                Assert.Equal(newIp, actualServer.Ip);
            }
        }

        [Fact]
        public void TestServerDelete() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);
                
                ServerRepository serverRepository = new ServerRepository(context, mapper);
                var actualServer = serverRepository.Delete(id: 1);

                Assert.Equal(data.server.Name, actualServer.Name);
                Assert.Equal(data.server.Ip, actualServer.Ip);
                Assert.Equal(data.server.GameName, actualServer.GameName);
                Assert.Equal(data.server.Status, actualServer.Status);
                Assert.Equal(data.server.HostingID, actualServer.HostingID);
                Assert.Equal(data.server.PlatformID, actualServer.PlatformID);
                Assert.Equal(data.server.CountryID, actualServer.CountryID);
                Assert.Equal(data.server.OwnerID, actualServer.OwnerID);

                var isServerWithSuchId = context.Server.Where(s => s.Id == 1).Any();
                Assert.False(isServerWithSuchId);
            }
        }

        [Fact]
        public void TestServerGetById() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);

                ServerRepository serverRepository = new ServerRepository(context, mapper);
                var actualServer = serverRepository.GetByID(1);

                Assert.Equal(data.server.Name, actualServer.Name);
                Assert.Equal(data.server.Ip, actualServer.Ip);
                Assert.Equal(data.server.GameName, actualServer.GameName);
                Assert.Equal(data.server.Status, actualServer.Status);
                Assert.Equal(data.server.HostingID, actualServer.HostingID);
                Assert.Equal(data.server.PlatformID, actualServer.PlatformID);
                Assert.Equal(data.server.CountryID, actualServer.CountryID);
                Assert.Equal(data.server.OwnerID, actualServer.OwnerID);
            }
        }

        [Fact]
        public void TestServerGetAll() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);

                ServerRepository serverRepository = new ServerRepository(context, mapper);
                var actualServers = serverRepository.GetAll();
                Assert.Single(actualServers);
            }
        }

        [Fact]
        public void TestServerGetByIP() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);

                ServerRepository serverRepository = new ServerRepository(context, mapper);
                var actualServer = serverRepository.GetByIP(data.server.Ip);

                Assert.Equal(data.server.Name, actualServer.Name);
                Assert.Equal(data.server.Ip, actualServer.Ip);
                Assert.Equal(data.server.GameName, actualServer.GameName);
                Assert.Equal(data.server.Status, actualServer.Status);
                Assert.Equal(data.server.HostingID, actualServer.HostingID);
                Assert.Equal(data.server.PlatformID, actualServer.PlatformID);
                Assert.Equal(data.server.CountryID, actualServer.CountryID);
                Assert.Equal(data.server.OwnerID, actualServer.OwnerID);
            }
        }

        [Fact]
        public void TestServerGetByName() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);

                ServerRepository serverRepository = new ServerRepository(context, mapper);
                var actualServer = serverRepository.GetByName(data.server.Name);

                Assert.Equal(data.server.Name, actualServer.Name);
                Assert.Equal(data.server.Ip, actualServer.Ip);
                Assert.Equal(data.server.GameName, actualServer.GameName);
                Assert.Equal(data.server.Status, actualServer.Status);
                Assert.Equal(data.server.HostingID, actualServer.HostingID);
                Assert.Equal(data.server.PlatformID, actualServer.PlatformID);
                Assert.Equal(data.server.CountryID, actualServer.CountryID);
                Assert.Equal(data.server.OwnerID, actualServer.OwnerID);
            }
        }

        [Fact]
        public void TestServerGetPlayersByServerID() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);

                ServerRepository serverRepository = new ServerRepository(context, mapper);
                var actualPlayers = serverRepository.GetPlayersByServerID(1);
                Assert.Single(actualPlayers);
            }
        }
    }
}
