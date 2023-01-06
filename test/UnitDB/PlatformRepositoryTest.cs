using Microsoft.EntityFrameworkCore;

using ServerING.Models;
using ServerING.Repository;


namespace UnitDB {
    public class PlatformRepositoryTest {
        private void LoadFixtures(TestData data, AppDBContent context) {
            context.Platform.AddRange(data.platform);
            context.SaveChanges();
        }

        [Fact]
        public void TestPlatformAdd() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                PlatformRepository platformRepository = new PlatformRepository(context);
                var actualPlatform = platformRepository.Add(data.platform);

                Assert.Equal(data.platform.Name, actualPlatform.Name);
                Assert.Equal(data.platform.Cost, actualPlatform.Cost);
                Assert.Equal(data.platform.Popularity, actualPlatform.Popularity);

                var isPlatformWithSuchName = context.Platform.Where(s => s.Name == data.platform.Name).Any();
                Assert.True(isPlatformWithSuchName);
            }
        }

        [Fact]
        public void TestPlatformUpdate() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);
                var newName = "test";

                Platform updatedPlatform = new Platform {
                    Id = 1,
                    Name = newName,
                    Cost = data.platform.Cost,
                    Popularity = data.platform.Popularity,
                };  

                PlatformRepository platformRepository = new PlatformRepository(context);
                var actualPlatform = platformRepository.Update(updatedPlatform);

                Assert.Equal(newName, updatedPlatform.Name);
            }
        }

        [Fact]
        public void TestPlatformDelete() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);
                
                PlatformRepository platformRepository = new PlatformRepository(context);
                var actualPlatform = platformRepository.Delete(id: 1);

                Assert.Equal(data.platform.Name, actualPlatform.Name);
                Assert.Equal(data.platform.Cost, actualPlatform.Cost);
                Assert.Equal(data.platform.Popularity, actualPlatform.Popularity);

                var isPlatformWithSuchId = context.Platform.Where(s => s.Id == 1).Any();
                Assert.False(isPlatformWithSuchId);
            }
        }

        [Fact]
        public void TestPlatformGetById() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);

                PlatformRepository platformRepository = new PlatformRepository(context);
                var actualPlatform = platformRepository.GetByID(1);

                Assert.Equal(data.platform.Name, actualPlatform.Name);
                Assert.Equal(data.platform.Cost, actualPlatform.Cost);
                Assert.Equal(data.platform.Popularity, actualPlatform.Popularity);
            }
        }

        [Fact]
        public void TestPlatformGetAll() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);

                PlatformRepository platformRepository = new PlatformRepository(context);
                var actualPlatforms = platformRepository.GetAll();
                Assert.Single(actualPlatforms);
            }
        }

        [Fact]
        public void TestPlatformGetByName() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);

                PlatformRepository platformRepository = new PlatformRepository(context);
                var actualPlatform = platformRepository.GetByName(data.platform.Name);

                Assert.Equal(data.platform.Name, actualPlatform.Name);
                Assert.Equal(data.platform.Cost, actualPlatform.Cost);
                Assert.Equal(data.platform.Popularity, actualPlatform.Popularity);
            }
        }
    }
}
