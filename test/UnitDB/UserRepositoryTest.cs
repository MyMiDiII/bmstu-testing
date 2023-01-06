using AutoMapper;
using Microsoft.EntityFrameworkCore;

using ServerING.Models;
using ServerING.Repository;
using ServerING.Utils;


namespace UnitDB {
    public class UserRepositoryTest {
        private void LoadFixtures(TestData data, AppDBContent context,
            bool loadUser = true, bool loadFavorites = true) {
            context.Country.AddRange(data.country);
            context.Platform.AddRange(data.platform);
            context.WebHosting.AddRange(data.hosting);
            context.Player.AddRange(data.player);
            context.ServerPlayer.AddRange(data.serverPlayer);
            context.Server.AddRange(data.server);

            if (loadUser)
                context.User.AddRange(data.user);

            if (loadFavorites)
                context.FavoriteServer.AddRange(data.favoriteServer);

            context.SaveChanges();
        }

        [Fact]
        public void TestUserAdd() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context, loadUser: false);

                UserRepository userRepository = new UserRepository(context);
                var actualUser = userRepository.Add(data.user);

                Assert.Equal(data.user.Login, actualUser.Login);
                Assert.Equal(data.user.Password, actualUser.Password);
                Assert.Equal(data.user.Role, actualUser.Role);

                var isUserWithSuchLogin = context.User.Where(s => s.Login == data.user.Login).Any();
                Assert.True(isUserWithSuchLogin);
            }
        }

        [Fact]
        public void TestUserUpdate() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);
                var newRole = "user";

                User updatedUser = new User {
                    Id = 1,
                    Login = data.user.Login,
                    Password = data.user.Password,
                    Role = newRole
                };

                UserRepository userRepository = new UserRepository(context);
                var actualUser = userRepository.Update(updatedUser);

                Assert.Equal(newRole, actualUser.Role);
            }
        }

        [Fact]
        public void TestUserDelete() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);
                
                UserRepository userRepository = new UserRepository(context);
                var actualUser = userRepository.Delete(id: 1);

                Assert.Equal(data.user.Login, actualUser.Login);
                Assert.Equal(data.user.Password, actualUser.Password);
                Assert.Equal(data.user.Role, actualUser.Role);

                var isUserWithSuchId = context.User.Where(s => s.Id == 1).Any();
                Assert.False(isUserWithSuchId);
            }
        }

        [Fact]
        public void TestUserGetById() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);

                UserRepository userRepository = new UserRepository(context);
                var actualUser = userRepository.GetByID(1);

                Assert.Equal(data.user.Login, actualUser.Login);
                Assert.Equal(data.user.Password, actualUser.Password);
                Assert.Equal(data.user.Role, actualUser.Role);
            }
        }

        [Fact]
        public void TestUserGetAll() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);

                UserRepository userRepository = new UserRepository(context);
                var actualUsers = userRepository.GetAll();
                Assert.Single(actualUsers);
            }
        }

        [Fact]
        public void TestUserGetByLogin() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);

                UserRepository userRepository = new UserRepository(context);
                var actualUser = userRepository.GetByLogin(data.user.Login);

                Assert.Equal(data.user.Login, actualUser.Login);
                Assert.Equal(data.user.Password, actualUser.Password);
                Assert.Equal(data.user.Role, actualUser.Role);
            }
        }

        [Fact]
        public void TestUserGetByRole() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);

                UserRepository userRepository = new UserRepository(context);
                var actualUsers = userRepository.GetByRole(data.user.Role);
                Assert.Single(actualUsers);
            }
        }

        [Fact]
        public void TestUserGetGetFavoriteServersByUserId() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);

                UserRepository userRepository = new UserRepository(context);
                var actualFavorites = userRepository.GetFavoriteServersByUserId(id: 1);
                Assert.Single(actualFavorites);
            }
        }

        [Fact]
        public void TestUserAddFavoriteServer() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context, loadFavorites: false);

                UserRepository userRepository = new UserRepository(context);
                var actualFavorite = userRepository.AddFavoriteServer(data.favoriteServer);

                Assert.Equal(data.favoriteServer.ServerID, actualFavorite.ServerID);
                Assert.Equal(data.favoriteServer.UserID, actualFavorite.UserID);
            }
        }

        [Fact]
        public void TestUserDeleteFavoriteServer() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);

                UserRepository userRepository = new UserRepository(context);
                var actualFavorite = userRepository.DeleteFavoriteServer(id: 1);

                Assert.Equal(data.favoriteServer.ServerID, actualFavorite.ServerID);
                Assert.Equal(data.favoriteServer.UserID, actualFavorite.UserID);
            }
        }

        [Fact]
        public void TestUserGetFavoriteServerByUserAndServerId() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);

                UserRepository userRepository = new UserRepository(context);
                var actualFavorite = userRepository.GetFavoriteServerByUserAndServerId(serverId: 1, userId: 1);
                
                Assert.Equal(data.favoriteServer.ServerID, actualFavorite.ServerID);
                Assert.Equal(data.favoriteServer.UserID, actualFavorite.UserID);
            }
        }
    }
}
