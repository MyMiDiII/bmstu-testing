using Microsoft.EntityFrameworkCore;
using Allure.Xunit.Attributes;

using ServerING.Models;
using ServerING.Repository;


namespace UnitDB {
    [AllureOwner("EqualNine")]
    [AllureSuite("Platform Repository Test")]
    public class PlatformRepositoryTest {
        private void LoadFixtures(TestData data, AppDBContent context) {
            context.Platform.AddRange(data.platform);
            context.SaveChanges();
        }

        // [Fact]
        [AllureXunit]
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

        // [Fact]
        [AllureXunit]
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

        // [Fact]
        [AllureXunit]
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

        // [Fact]
        [AllureXunit]
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

        // [Fact]
        [AllureXunit]
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

        // [Fact]
        [AllureXunit]
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
