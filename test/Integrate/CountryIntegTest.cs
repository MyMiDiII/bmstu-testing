using ServerING.Services;
using ServerING.ModelsBL;
using Integrate.Utils;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Allure.Xunit.Attributes;
using AutoFixture;


namespace Integrate {
    [AllureOwner("EqualNine")]
    [AllureSuite("Country Integration Tests")]
    public class CountryIntegTest {
        private CountryService countryService;

        public CountryIntegTest() {
            var server = new TestServer(new WebHostBuilder().UseStartup<TestStartup>());
            countryService = (CountryService)server.Services.GetRequiredService<ICountryService>();
        }

        [AllureXunit]
        [ResetDatabase]
        public void TestCountryAdd() {
            var fixture = new Fixture();
            CountryBL country = fixture.Create<CountryBL>();

            CountryBL result = countryService.AddCountry(country);

            Assert.Equal(country.Name, result.Name);
            Assert.Equal(country.OverallPlayers, result.OverallPlayers);
            Assert.Equal(country.LevelOfInterest, result.LevelOfInterest);
        }

        [AllureXunitTheory]
        [InlineData(1)]
        [ResetDatabase]
        public void TestCountryUpdate(int id) {
            var fixture = new Fixture();
            CountryBL country = fixture.Create<CountryBL>();
            country.Id = id;

            CountryBL result = countryService.UpdateCountry(id, country);

            Assert.Equal(country.Name, result.Name);
            Assert.Equal(country.OverallPlayers, result.OverallPlayers);
            Assert.Equal(country.LevelOfInterest, result.LevelOfInterest);
        }

        [AllureXunitTheory]
        [InlineData(1)]
        [ResetDatabase]
        public void TestCountryDelete(int id) {
            CountryBL result = countryService.DeleteCountry(id);

            Assert.NotNull(result);
        }

        [AllureXunitTheory]
        [InlineData(1)]
        [ResetDatabase]
        public void TestCountryGetById(int id) {
            var result = countryService.GetCountryByID(id);

            Assert.NotNull(result);
        }

        [AllureXunit]
        [ResetDatabase]
        public void TestCountryGetAll() {
            var result = countryService.GetAllCountries();

            Assert.NotNull(result);
        }

        [AllureXunitTheory]
        [InlineData("Country1")]
        [ResetDatabase]
        public void TestCountryGetByName(String name) {
            var result = countryService.GetCountryByName(name);

            Assert.NotNull(result);
        }
    }
}
