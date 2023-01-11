using Microsoft.EntityFrameworkCore;
using Allure.Xunit.Attributes;

using ServerING.Models;
using ServerING.Repository;


namespace UnitDB {
    [AllureOwner("EqualNine")]
    [AllureSuite("Hosting Repository Test")]
    public class HostingRepositoryTest {
        private void LoadFixtures(TestData data, AppDBContent context) {
            context.WebHosting.AddRange(data.hosting);
            context.SaveChanges();
        }

        // [Fact]
        [AllureXunit]
        public void TestHostingAdd() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                HostingRepository hostingRepository = new HostingRepository(context);
                var actualHosting = hostingRepository.Add(data.hosting);

                Assert.Equal(data.hosting.Name, actualHosting.Name);
                Assert.Equal(data.hosting.SubMonths, actualHosting.SubMonths);
                Assert.Equal(data.hosting.PricePerMonth, actualHosting.PricePerMonth);

                var isHostingWithSuchName = context.WebHosting.Where(s => s.Name == data.hosting.Name).Any();
                Assert.True(isHostingWithSuchName);
            }
        }

        // [Fact]
        [AllureXunit]
        public void TestHostingUpdate() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);
                var newName = "test";

                WebHosting updatedHosting = new WebHosting {
                    Id = 1,
                    Name = newName,
                    SubMonths = data.hosting.SubMonths,
                    PricePerMonth = data.hosting.PricePerMonth,
                };  

                HostingRepository hostingRepository = new HostingRepository(context);
                var actualHosting = hostingRepository.Update(updatedHosting);

                Assert.Equal(newName, updatedHosting.Name);
            }
        }

        // [Fact]
        [AllureXunit]
        public void TestHostingDelete() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);
                
                HostingRepository hostingRepository = new HostingRepository(context);
                var actualHosting = hostingRepository.Delete(id: 1);

                Assert.Equal(data.hosting.Name, actualHosting.Name);
                Assert.Equal(data.hosting.SubMonths, actualHosting.SubMonths);
                Assert.Equal(data.hosting.PricePerMonth, actualHosting.PricePerMonth);

                var isHostingWithSuchId = context.WebHosting.Where(s => s.Id == 1).Any();
                Assert.False(isHostingWithSuchId);
            }
        }

        // [Fact]
        [AllureXunit]
        public void TestHostingGetById() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);

                HostingRepository hostingRepository = new HostingRepository(context);
                var actualHosting = hostingRepository.GetByID(1);

                Assert.Equal(data.hosting.Name, actualHosting.Name);
                Assert.Equal(data.hosting.SubMonths, actualHosting.SubMonths);
                Assert.Equal(data.hosting.PricePerMonth, actualHosting.PricePerMonth);
            }
        }

        // [Fact]
        [AllureXunit]
        public void TestHostingGetAll() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);

                HostingRepository hostingRepository = new HostingRepository(context);
                var actualHostings = hostingRepository.GetAll();
                Assert.Single(actualHostings);
            }
        }

        // [Fact]
[       AllureXunit]
        public void TestHostingGetByName() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);

                HostingRepository hostingRepository = new HostingRepository(context);
                var actualHosting = hostingRepository.GetByName(data.hosting.Name);

                Assert.Equal(data.hosting.Name, actualHosting.Name);
                Assert.Equal(data.hosting.SubMonths, actualHosting.SubMonths);
                Assert.Equal(data.hosting.PricePerMonth, actualHosting.PricePerMonth);
            }
        }
    }
}
