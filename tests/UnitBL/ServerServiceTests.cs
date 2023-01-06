using AutoMapper;
using Moq;

using Allure.Xunit.Attributes;

using ObjectMothers;

using ServerING.Utils;
using ServerING.Services;
using ServerING.Exceptions;
using ServerING.Models;
using ServerING.DTO;
using ServerING.Interfaces;

namespace UnitBL
{
    [AllureOwner("EqualNine")]
    [AllureSuite("Server Service Tests")]
    public class ServerServiceTests 
    {
        private IMapper _mapper;
        private IEnumerable<Server> _servers;

        public ServerServiceTests() {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMappingProfile());
            });
            _mapper = mockMapper.CreateMapper();

            var server1 = ServersOM.NumberedServer(1).build();
            var server2 = ServersOM.NumberedServer(2).build();
            _servers = new List<Server> { server1, server2 };
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
        public void TestProcessServers() {
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
        }
    }
}