using Microsoft.EntityFrameworkCore;
using Allure.Xunit.Attributes;

using ServerING.Models;
using ServerING.Repository;


namespace UnitDB {
    [AllureOwner("EqualNine")]
    [AllureSuite("Country Repository Test")]
    public class CountryRepositoryTest {
        private void LoadFixtures(TestData data, AppDBContent context) {
            context.Country.AddRange(data.country);
            context.SaveChanges();
        }

        // [Fact]
        [AllureXunit]
        public void TestCountryAdd() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                CountryRepository countryRepository = new CountryRepository(context);
                var actualCountry = countryRepository.Add(data.country);

                Assert.Equal(data.country.Name, actualCountry.Name);
                Assert.Equal(data.country.LevelOfInterest, actualCountry.LevelOfInterest);
                Assert.Equal(data.country.OverallPlayers, actualCountry.OverallPlayers);

                var isCountryWithSuchName = context.Country.Where(s => s.Name == data.country.Name).Any();
                Assert.True(isCountryWithSuchName);
            }
        }

        // [Fact]
        [AllureXunit]
        public void TestCountryUpdate() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);
                var newName = "test";

                Country updatedCountry = new Country {
                    Id = 1,
                    Name = newName,
                    OverallPlayers = data.country.OverallPlayers,
                    LevelOfInterest = data.country.LevelOfInterest,
                };  

                CountryRepository countryRepository = new CountryRepository(context);
                var actualCountry = countryRepository.Update(updatedCountry);

                Assert.Equal(newName, actualCountry.Name);
            }
        }

        // [Fact]
        [AllureXunit]
        public void TestCountryDelete() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);
                
                CountryRepository countryRepository = new CountryRepository(context);
                var actualCountry = countryRepository.Delete(id: 1);

                Assert.Equal(data.country.Name, actualCountry.Name);
                Assert.Equal(data.country.LevelOfInterest, actualCountry.LevelOfInterest);
                Assert.Equal(data.country.OverallPlayers, actualCountry.OverallPlayers);

                var isCountryWithSuchId = context.User.Where(s => s.Id == 1).Any();
                Assert.False(isCountryWithSuchId);
            }
        }

        // [Fact]
        [AllureXunit]
        public void TestCountryGetById() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);

                CountryRepository countryRepository = new CountryRepository(context);
                var actualCountry = countryRepository.GetByID(1);

                Assert.Equal(data.country.Name, actualCountry.Name);
                Assert.Equal(data.country.LevelOfInterest, actualCountry.LevelOfInterest);
                Assert.Equal(data.country.OverallPlayers, actualCountry.OverallPlayers);
            }
        }

        // [Fact]
        [AllureXunit]
        public void TestCountryGetAll() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);

                CountryRepository countryRepository = new CountryRepository(context);
                var actualCountries = countryRepository.GetAll();
                Assert.Single(actualCountries);
            }
        }

        // [Fact]
        [AllureXunit]
        public void TestCountryGetByName() {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseInMemoryDatabase(databaseName: myDatabaseName)
                .Options;
            var data = new TestData();

            using (var context = new AppDBContent(options)) {
                LoadFixtures(data, context);

                CountryRepository countryRepository = new CountryRepository(context);
                var actualCountry = countryRepository.GetByName(data.country.Name);

                Assert.Equal(data.country.Name, actualCountry.Name);
                Assert.Equal(data.country.LevelOfInterest, actualCountry.LevelOfInterest);
                Assert.Equal(data.country.OverallPlayers, actualCountry.OverallPlayers);
            }
        }
    }
}
