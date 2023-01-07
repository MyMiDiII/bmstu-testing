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
    [AllureSuite("User Service Tests")]
    public class UserServiceTests 
    {
        private IMapper _mapper;
        private IEnumerable<User> _users;
        private IEnumerable<UserBL> _usersBL;
        private Mock<IServerService> _mockServerService;

        public UserServiceTests() {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMappingProfile());
            });
            _mapper = mockMapper.CreateMapper();

            var user1 = UsersOM.Guest(1).build();
            var user2 = UsersOM.User(2).build();
            var user3 = UsersOM.Admin(3).build();
            _users = new List<User>() { user1, user2, user3 };

            var userBL1 = UsersOM.Guest(1).buildBL();
            var userBL2 = UsersOM.User(2).buildBL();
            var userBL3 = UsersOM.Admin(3).buildBL();
            _usersBL = new List<UserBL>() { userBL1, userBL2, userBL3 };

            _mockServerService = new Mock<IServerService>();
        }

        [AllureXunit]
        public void TestUserAdd() {
            // Arrange
            var userBLNew = UsersOM.User(4).withId(0).buildBL();

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetAll()).Returns(_users);
            var userService = new UserService(mockUserRepository.Object, _mockServerService.Object, _mapper);

            // Act
            userService.AddUser(userBLNew);

            // Assert
            mockUserRepository.Verify(repo => repo.GetAll(), Times.Once);
            mockUserRepository.Verify(repo => repo.Add(It.IsAny<User>()), Times.Once);
        }

        [AllureXunit]
        public void TestUserAddExists() {
            // Arrange
            var userBLNew = UsersOM.User(2).withId(0).buildBL();

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetAll()).Returns(_users);
            var userService = new UserService(mockUserRepository.Object, _mockServerService.Object, _mapper);

            // Act
            void action() => userService.AddUser(userBLNew);

            // Assert
            Assert.Throws<UserAlreadyExistsException>(action);
            mockUserRepository.Verify(repo => repo.GetAll(), Times.Once);
            mockUserRepository.Verify(repo => repo.Add(It.IsAny<User>()), Times.Never);
        }

        [AllureXunit]
        public void TestUserDelete() {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var userService = new UserService(mockUserRepository.Object, _mockServerService.Object, _mapper);

            // Act
            userService.DeleteUser(2);

            // Assert
            mockUserRepository.Verify(repo => repo.Delete(2), Times.Once);
        }

        [AllureXunit]
        public void TestUserUpdate() {
            // Arrange
            var userBLNew = UsersOM.User(2).withRole("admin").buildBL();

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetByID(2)).Returns(
                _users.First(item => item.Id == 2));
            mockUserRepository.Setup(repo => repo.GetAll()).Returns(_users);
            var userService = new UserService(mockUserRepository.Object, _mockServerService.Object, _mapper);

            // Act
            userService.UpdateUser(2, userBLNew);

            // Assert
            mockUserRepository.Verify(repo => repo.GetByID(2), Times.Once);
            mockUserRepository.Verify(repo => repo.GetAll(), Times.Once);
            mockUserRepository.Verify(repo => repo.Update(It.IsAny<User>()), Times.Once);
        }

        [AllureXunit]
        public void TestUserGetById() {
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetByID(1)).Returns(
                _users.First(item => item.Id == 1));
            var userService = new UserService(mockUserRepository.Object, _mockServerService.Object, _mapper);

            // Act
            userService.GetUserByID(1);

            // Assert
            mockUserRepository.Verify(repo => repo.GetByID(1), Times.Once);
        }

        [AllureXunit]
        public void TestUserGetAll() {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetAll()).Returns(_users);
            var userService = new UserService(mockUserRepository.Object, _mockServerService.Object, _mapper);

            // Act
            userService.GetAllUsers();

            // Assert
            mockUserRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        [AllureXunit]
        public void TestUserGetByLogin() {
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetByLogin("user2")).Returns(
                _users.First(item => item.Login == "user2"));
            var userService = new UserService(mockUserRepository.Object, _mockServerService.Object, _mapper);

            // Act
            userService.GetUserByLogin("user2");

            // Assert
            mockUserRepository.Verify(repo => repo.GetByLogin("user2"), Times.Once);
        }

        [AllureXunit]
        public void TestUserGetByRole() {
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetByRole("admin")).Returns(
                _users.Where(item => item.Role == "admin"));
            var userService = new UserService(mockUserRepository.Object, _mockServerService.Object, _mapper);

            // Act
            userService.GetUsersByRole("admin");

            // Assert
            mockUserRepository.Verify(repo => repo.GetByRole("admin"), Times.Once);
        }

        [AllureXunit]
        public void TestUserLogin() {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetByLogin(It.IsAny<string>())).Returns(
                (string login) => _users.First(item => item.Login == login));
            var userService = new UserService(mockUserRepository.Object, _mockServerService.Object, _mapper);
            var loginDto = new LoginDto() { Login = "user2", Password = "password2"};

            // Act
            var user = userService.Login(loginDto);

            //Assert
            mockUserRepository.Verify(repo => repo.GetByLogin("user2"), Times.Once);
            Assert.NotNull(user);
        }

        [AllureXunit]
        public void TestUserLoginNoUser() {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetByLogin(It.IsAny<string>())).Returns(
                (string login) => _users.FirstOrDefault(item => item.Login == login));
            var userService = new UserService(mockUserRepository.Object, _mockServerService.Object, _mapper);
            var loginDto = new LoginDto() { Login = "user1", Password = "password1"};

            // Act
            var user = userService.Login(loginDto);

            //Assert
            mockUserRepository.Verify(repo => repo.GetByLogin("user1"), Times.Once);
            Assert.Null(user);
        }

        [AllureXunit]
        public void TestUserLoginWrongPassword() {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetByLogin(It.IsAny<string>())).Returns(
                (string login) => _users.First(item => item.Login == login));
            var userService = new UserService(mockUserRepository.Object, _mockServerService.Object, _mapper);
            var loginDto = new LoginDto() { Login = "user2", Password = "password1"};

            // Act
            var user = userService.Login(loginDto);

            //Assert
            mockUserRepository.Verify(repo => repo.GetByLogin("user2"), Times.Once);
            Assert.Null(user);
        }

        [AllureXunit]
        public void TestGetUserFavoritesServers() {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetByID(2)).Returns(
                _users.First(item => item.Id == 2));
            mockUserRepository.Setup(repo => repo.GetAll()).Returns(_users);
            var userService = new UserService(mockUserRepository.Object, _mockServerService.Object, _mapper);
            var filter = new ServerFilterDto();

            // Act
            userService.GetUserFavoriteServers(2, filter, null, null, null);

            // Assert
            mockUserRepository.Verify(repo => repo.GetByID(2), Times.Once);
            mockUserRepository.Verify(repo => repo.GetFavoriteServersByUserId(2), Times.Once);
            _mockServerService.Verify(
                service => 
                service.ProcessServers(It.IsAny<IEnumerable<ServerBL>>()
                                     , It.IsAny<ServerFilterDto>()
                                     , null, null, null), Times.Once);
        }

        [AllureXunit]
        public void TestAddUserFavoritesServers() {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var userService = new UserService(mockUserRepository.Object, _mockServerService.Object, _mapper);
            var filter = new ServerFilterDto();

            // Act
            userService.AddFavoriteServer(2, 1);

            // Assert
            mockUserRepository.Verify(repo => repo.GetFavoriteServerByUserAndServerId(2, 1), Times.Once);
            _mockServerService.Verify(serv => serv.UpdateServerRating(1, 1));
            mockUserRepository.Verify(repo => repo.AddFavoriteServer(It.IsAny<FavoriteServer>()), Times.Once);
        }

        [AllureXunit]
        public void TestDeleteUserFavoritesServers() {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x =>
                x.GetFavoriteServerByUserAndServerId(It.IsAny<int>(), It.IsAny<int>())).Returns(
                    (int userId, int serverId) => new FavoriteServer() {
                        Id = 0,
                        UserID = userId,
                        ServerID = serverId
                    }
                );
            var userService = new UserService(mockUserRepository.Object, _mockServerService.Object, _mapper);

            // Act
            userService.DeleteFavoriteServer(2, 1);

            // Assert
            _mockServerService.Verify(serv => serv.UpdateServerRating(1, -1));
            mockUserRepository.Verify(repo => repo.GetFavoriteServerByUserAndServerId(2, 1), Times.Once);
            mockUserRepository.Verify(repo => repo.DeleteFavoriteServer(0), Times.Once);
        }

        [AllureXunit]
        public void TestGetFavoriteByServerAndUserId() {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var userService = new UserService(mockUserRepository.Object, _mockServerService.Object, _mapper);

            // Act
            userService.GetFavoriteByServerAndUserId(2, 1);

            // Assert
            mockUserRepository.Verify(repo => repo.GetFavoriteServerByUserAndServerId(2, 1), Times.Once);
        }
    }
}