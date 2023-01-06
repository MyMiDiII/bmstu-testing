using AutoMapper;
using Moq;

using Allure.Xunit.Attributes;

using ObjectMothers;

using ServerING.Utils;
using ServerING.Services;
using ServerING.Exceptions;
using ServerING.Models;
using ServerING.ModelsBL;
using ServerING.DTO;
using ServerING.Enums;
using ServerING.Interfaces;

namespace UnitBL
{
    [AllureOwner("EqualNine")]
    [AllureSuite("Server Service Tests")]
    public class ServerServiceTests 
    {
        private IMapper _mapper;
        private IEnumerable<Server> _servers;
        private IEnumerable<ServerBL> _serversBL;

        public ServerServiceTests() {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMappingProfile());
            });
            _mapper = mockMapper.CreateMapper();

            var server1 = ServersOM.NumberedServer(1).build();
            var server2 = ServersOM.NumberedServer(2).build();
            var server13 = ServersOM.NumberedServer(13).build();
            _servers = new List<Server> { server1, server2, server13 };

            var serverBL1 = ServersOM.NumberedServer(1).buildBL();
            var serverBL2 = ServersOM.NumberedServer(2).buildBL();
            var serverBL13 = ServersOM.NumberedServer(13).buildBL();
            _serversBL = new List<ServerBL> { serverBL1, serverBL2, serverBL13 };
        }

        [AllureXunit]
        public void TestServerAdd() {
            // Arrange
            var serverBLNew = ServersOM.NumberedServer(3).withId(0).buildBL();

            var mockServerRepository = new Mock<IServerRepository>();
            mockServerRepository.Setup(repo => repo.GetAll()).Returns(_servers);
            var serverService = new ServerService(mockServerRepository.Object, _mapper);

            // Act
            serverService.AddServer(serverBLNew);

            // Assert
            mockServerRepository.Verify(repo => repo.GetAll(), Times.Once);
            mockServerRepository.Verify(repo => repo.Add(It.IsAny<Server>()), Times.Once);
        }

        [AllureXunit]
        public void TestServerAddExists() {
            // Arrange
            var serverBLNew = ServersOM.NumberedServer(1).withId(0).buildBL();

            var mockServerRepository = new Mock<IServerRepository>();
            mockServerRepository.Setup(repo => repo.GetAll()).Returns(_servers);
            mockServerRepository.Setup(repo => repo.GetByIP(It.IsAny<string>())).Returns(
               (string ip) => _servers.First(item => item.Ip == ip));
            var serverService = new ServerService(mockServerRepository.Object, _mapper);

            // Act
            void action() => serverService.AddServer(serverBLNew);

            // Assert
            Assert.Throws<ServerConflictException>(action);
            mockServerRepository.Verify(repo => repo.GetAll(), Times.Once);
            mockServerRepository.Verify(repo => repo.GetByIP(It.IsAny<string>()), Times.Once);
            mockServerRepository.Verify(repo => repo.Add(It.IsAny<Server>()), Times.Never);
        }

        [AllureXunit]
        public void TestServerDelete() {
            // Arrange
            var mockServerRepository = new Mock<IServerRepository>();
            var serverService = new ServerService(mockServerRepository.Object, _mapper);

            // Act
            serverService.DeleteServer(1);

            // Assert
            mockServerRepository.Verify(repo => repo.Delete(1), Times.Once);
        }

        [AllureXunit]
        public void TestServerUpdate() {
            // Arrange
            var serverBLNew = ServersOM.NumberedServer(3).withId(1).buildBL();

            var mockServerRepository = new Mock<IServerRepository>();
            mockServerRepository.Setup(repo => repo.GetByID(1)).Returns(
                _servers.First(item => item.Id == 1));
            mockServerRepository.Setup(repo => repo.GetAll()).Returns(_servers);
            var serverService = new ServerService(mockServerRepository.Object, _mapper);

            // Act
            serverService.UpdateServer(serverBLNew);

            // Assert
            mockServerRepository.Verify(repo => repo.GetByID(1), Times.Once);
            mockServerRepository.Verify(repo => repo.GetAll(), Times.Once);
            mockServerRepository.Verify(repo => repo.Update(It.IsAny<Server>()), Times.Once);
        }

        [AllureXunit]
        public void TestServerGetById() {
            var mockServerRepository = new Mock<IServerRepository>();
            mockServerRepository.Setup(repo => repo.GetByID(1)).Returns(
                _servers.First(item => item.Id == 1));
            var serverService = new ServerService(mockServerRepository.Object, _mapper);

            // Act
            serverService.GetServerByID(1);

            // Assert
            mockServerRepository.Verify(repo => repo.GetByID(1), Times.Once);
        }

        [AllureXunit]
        public void TestServerGetAll() {
            // Arrange
            var filter = new ServerFilterDto();
            var mockServerRepository = new Mock<IServerRepository>();
            mockServerRepository.Setup(repo => repo.GetAll()).Returns(_servers);
            var serverService = new ServerService(mockServerRepository.Object, _mapper);

            // Act
            serverService.GetAllServers(filter, null, null, null);

            // Assert
            mockServerRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        [AllureXunit]
        public void TestServerGetByName() {
            var mockServerRepository = new Mock<IServerRepository>();
            mockServerRepository.Setup(repo => repo.GetByName("Server1")).Returns(
                _servers.First(item => item.Name == "Server1"));
            var serverService = new ServerService(mockServerRepository.Object, _mapper);

            // Act
            serverService.GetServerByName("Server1");

            // Assert
            mockServerRepository.Verify(repo => repo.GetByName("Server1"), Times.Once);
        }

        [AllureXunit]
        public void TestProcessServersFilter() {
            // Arrange
            var mockServerRepository = new Mock<IServerRepository>();
            var serverService = new ServerService(mockServerRepository.Object, _mapper);
            var filter = new ServerFilterDto() { Name = "1" };

            // Act
            var filteredServers = serverService.ProcessServers(_serversBL, filter, null, null, null);

            //Assert
            Assert.Equal(2, filteredServers.Count());
            Assert.All(filteredServers, item => Assert.Contains("1", item.Name));
        }

        [AllureXunit]
        public void TestProcessServersSort() {
            // Arrange
            var mockServerRepository = new Mock<IServerRepository>();
            var serverService = new ServerService(mockServerRepository.Object, _mapper);
            var filter = new ServerFilterDto();

            // Act
            var sortedServers = serverService.ProcessServers(
                                    _serversBL, filter, ServerSortState.NameDesc, null, null);

            //Assert
            Assert.Equal(3, sortedServers.Count());
            Assert.Collection(sortedServers,
                              item => Assert.Equal("Server2", item.Name),
                              item => Assert.Equal("Server13", item.Name),
                              item => Assert.Equal("Server1", item.Name)
                              );
        }

        [AllureXunit]
        public void TestProcessServersPagination() {
            // Arrange
            var mockServerRepository = new Mock<IServerRepository>();
            var serverService = new ServerService(mockServerRepository.Object, _mapper);
            var filter = new ServerFilterDto();

            // Act
            var sortedServers = serverService.ProcessServers(
                                    _serversBL, filter, null, 2, 2);

            //Assert
            Assert.Equal(1, sortedServers.Count());
            Assert.Collection(sortedServers, item => Assert.Equal("Server13", item.Name));
        }

        [AllureXunit]
        public void TestGetServerPlayers() {
            // Arrange
            var mockServerRepository = new Mock<IServerRepository>();
            mockServerRepository.Setup(repo => repo.GetByID(1)).Returns(
                _servers.First(item => item.Id == 1));
            mockServerRepository.Setup(repo => repo.GetAll()).Returns(_servers);
            var serverService = new ServerService(mockServerRepository.Object, _mapper);

            // Act
            serverService.GetServerPlayers(1);

            // Assert
            mockServerRepository.Verify(repo => repo.GetByID(1), Times.Once);
            mockServerRepository.Verify(repo => repo.GetPlayersByServerID(1), Times.Once);
        }

        [AllureXunit]
        public void TestUpdateServerRating() {
            // Arrange
            var serverBLNew = ServersOM.NumberedServer(3).withId(1).buildBL();

            var mockServerRepository = new Mock<IServerRepository>();
            mockServerRepository.Setup(repo => repo.GetByID(1)).Returns(
                _servers.First(item => item.Id == 1));
            var serverService = new ServerService(mockServerRepository.Object, _mapper);

            // Act
            serverService.UpdateServerRating(1, 10);

            // Assert
            mockServerRepository.Verify(repo => repo.GetByID(1), Times.Once);
            mockServerRepository.Verify(repo => repo.Update(It.IsAny<Server>()), Times.Once);
        }
    }
}