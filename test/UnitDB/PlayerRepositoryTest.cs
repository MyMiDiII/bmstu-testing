using Microsoft.EntityFrameworkCore;
using Allure.Xunit.Attributes;

using ServerING.Models;
using ServerING.Repository;


namespace UnitDB {
    [AllureOwner("EqualNine")]
    [AllureSuite("Player Repository Test")]
    public class PlayerRepositoryTest {
        private void LoadFixtures(TestData data, AppDBContent context) {
            context.Player.AddRange(data.player);
            context.SaveChanges();
        }

        // [Fact]
        [AllureXunit]
        public void TestPlayerAdd() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                PlayerRepository playerRepository = new PlayerRepository(context);
                var actualPlayer = playerRepository.Add(data.player);

                Assert.Equal(data.player.Nickname, actualPlayer.Nickname);
                Assert.Equal(data.player.LastPlayed, actualPlayer.LastPlayed);
                Assert.Equal(data.player.HoursPlayed, actualPlayer.HoursPlayed);

                var isPlayerWithSuchName = context.Player.Where(s => s.Nickname == data.player.Nickname).Any();
                Assert.True(isPlayerWithSuchName);
            }
        }

        // [Fact]
        [AllureXunit]
        public void TestPlayerUpdate() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);
                var newName = "test";

                Player updatedPlayer = new Player {
                    Id = 1,
                    Nickname = newName,
                    LastPlayed = data.player.LastPlayed,
                    HoursPlayed = data.player.HoursPlayed,
                };  

                PlayerRepository playerRepository = new PlayerRepository(context);
                var actualPlayer = playerRepository.Update(updatedPlayer);

                Assert.Equal(newName, updatedPlayer.Nickname);
            }
        }

        // [Fact]
        [AllureXunit]
        public void TestPlayerDelete() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);
                
                PlayerRepository playerRepository = new PlayerRepository(context);
                var actualPlayer = playerRepository.Delete(id: 1);

                Assert.Equal(data.player.Nickname, actualPlayer.Nickname);
                Assert.Equal(data.player.LastPlayed, actualPlayer.LastPlayed);
                Assert.Equal(data.player.HoursPlayed, actualPlayer.HoursPlayed);

                var isPlayerWithSuchId = context.Player.Where(s => s.Id == 1).Any();
                Assert.False(isPlayerWithSuchId);
            }
        }

        // [Fact]
        [AllureXunit]
        public void TestPlayerGetById() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);

                PlayerRepository playerRepository = new PlayerRepository(context);
                var actualPlayer = playerRepository.GetByID(1);

                Assert.Equal(data.player.Nickname, actualPlayer.Nickname);
                Assert.Equal(data.player.LastPlayed, actualPlayer.LastPlayed);
                Assert.Equal(data.player.HoursPlayed, actualPlayer.HoursPlayed);
            }
        }

        // [Fact]
        [AllureXunit]
        public void TestPlayerGetAll() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);

                PlayerRepository playerRepository = new PlayerRepository(context);
                var actualPlayers = playerRepository.GetAll();
                Assert.Single(actualPlayers);
            }
        }

        // [Fact]
        [AllureXunit]
        public void TestPlayerGetByName() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);

                PlayerRepository playerRepository = new PlayerRepository(context);
                var actualPlayer = playerRepository.GetByNickname(data.player.Nickname);

                Assert.Equal(data.player.Nickname, actualPlayer.Nickname);
                Assert.Equal(data.player.LastPlayed, actualPlayer.LastPlayed);
                Assert.Equal(data.player.HoursPlayed, actualPlayer.HoursPlayed);
            }
        }
    }
}
