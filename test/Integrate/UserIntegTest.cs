using ServerING.Services;
using ServerING.ModelsBL;
using Integrate.Utils;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Allure.Xunit.Attributes;
using AutoFixture;
using ServerING.DTO;


namespace Integrate {
    [AllureOwner("EqualNine")]
    [AllureSuite("User Integration Tests")]
    public class UserIntegTest {
        private UserService userService;

        public UserIntegTest() {
            var server = new TestServer(new WebHostBuilder().UseStartup<TestStartup>());
            userService = (UserService)server.Services.GetRequiredService<IUserService>();
        }

        [AllureXunit]
        [ResetDatabase]
        public void TestUserAdd() {
            var fixture = new Fixture();
            UserBL user = fixture.Create<UserBL>();

            UserBL result = userService.AddUser(user);

            Assert.Equal(user.Login, result.Login);
            Assert.Equal(user.Password, result.Password);
            Assert.Equal(user.Role, result.Role);
        }

        [AllureXunitTheory]
        [InlineData(1)]
        [ResetDatabase]
        public void TestUserUpdate(int id) {
            var fixture = new Fixture();
            UserBL user = fixture.Create<UserBL>();
            user.Id = id;

            UserBL result = userService.UpdateUser(id, user);

            Assert.Equal(user.Login, result.Login);
            Assert.Equal(user.Password, result.Password);
            Assert.Equal(user.Role, result.Role);
        }

        [AllureXunitTheory]
        [InlineData(1)]
        [ResetDatabase]
        public void TestUserDelete(int id) {
            UserBL result = userService.DeleteUser(id);

            Assert.NotNull(result);
        }

        [AllureXunitTheory]
        [InlineData(1)]
        [ResetDatabase]
        public void TestUserGetById(int id) {
            var result = userService.GetUserByID(id);

            Assert.NotNull(result);
        }

        [AllureXunit]
        [ResetDatabase]
        public void TestUserGetAll() {
            var result = userService.GetAllUsers();

            Assert.NotNull(result);
        }

        [AllureXunitTheory]
        [InlineData("User1")]
        [ResetDatabase]
        public void TestUserGetByName(String login) {
            var result = userService.GetUserByLogin(login);

            Assert.NotNull(result);
            Assert.Equal(login, result.Login);
        }

        [AllureXunitTheory]
        [InlineData("Role1")]
        [ResetDatabase]
        public void TestUserGetByRole(String role) {
            var result = userService.GetUsersByRole(role);

            Assert.NotNull(result);

            foreach (var user in result) {
                Assert.Equal(role, user.Role);
            }
        }

        [AllureXunit]
        [ResetDatabase]
        public void TestUserLogin() {
            var loginDto = new LoginDto() {Login = "User1", Password = "Password1"};
            var result = userService.Login(loginDto);

            Assert.NotNull(result);
            Assert.Equal(loginDto.Login, result.Login);
            Assert.Equal(loginDto.Password, result.Password);
        }

        [AllureXunitTheory]
        [InlineData(1)]
        [ResetDatabase]
        public void TestUserGetUserFavoriteServers(int id) {
            var filter = new ServerFilterDto();

            var result = userService.GetUserFavoriteServers(id, filter, null, null, null);

            Assert.NotNull(result);
        }

        [AllureXunitTheory]
        [InlineData(1, 2)]
        [ResetDatabase]
        public void TestUserAddFavoriteServer(int userId, int serverId) {
            var result = userService.AddFavoriteServer(userId, serverId);

            Assert.Equal(serverId, result.ServerID);
            Assert.Equal(userId, result.UserID);
        }

        [AllureXunitTheory]
        [InlineData(1, 1)]
        [ResetDatabase]
        public void TestUserDeleteFavoriteServer(int userId, int serverId) {
            var result = userService.DeleteFavoriteServer(userId, serverId);

            Assert.Equal(serverId, result.ServerID);
            Assert.Equal(userId, result.UserID);
        }

        [AllureXunitTheory]
        [InlineData(1, 1)]
        [ResetDatabase]
        public void TestUserGetFavoriteByServerAndUserId(int userId, int serverId) {
            var result = userService.GetFavoriteByServerAndUserId(userId, serverId);

            Assert.Equal(serverId, result.ServerID);
            Assert.Equal(userId, result.UserID);
        }
    }
}
