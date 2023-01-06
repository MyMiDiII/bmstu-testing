using AutoMapper;
using Moq;

using Allure.Xunit.Attributes;

using ObjectMothers;

using ServerING.Utils;
using ServerING.Services;
using ServerING.Exceptions;
using ServerING.Models;
using ServerING.Interfaces;

namespace UnitBL
{
    [AllureOwner("EqualNine")]
    [AllureSuite("Country Service Tests")]
    public class CountryServiceTests 
    {
        private IMapper _mapper;
        private IEnumerable<Country> _countries;

        public CountryServiceTests() {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMappingProfile());
            });
            _mapper = mockMapper.CreateMapper();

            var country1 = CountriesOM.NumberedCountry(1).build();
            var country2 = CountriesOM.NumberedCountry(2).build();
            _countries = new List<Country> { country1, country2 };
        }

        [AllureXunit]
        public void TestCountryAdd() {
            // Arrange
            var countryBLNew = CountriesOM.NumberedCountry(3).withId(0).buildBL();

            var mockCountryRepository = new Mock<ICountryRepository>();
            mockCountryRepository.Setup(repo => repo.GetAll()).Returns(_countries);
            var countryService = new CountryService(mockCountryRepository.Object, _mapper);

            // Act
            countryService.AddCountry(countryBLNew);

            // Assert
            mockCountryRepository.Verify(repo => repo.GetAll(), Times.Once);
            mockCountryRepository.Verify(repo => repo.Add(It.IsAny<Country>()), Times.Once);
        }

        [AllureXunit]
        public void TestCountryAddExists() {
            // Arrange
            var countryBLNew = CountriesOM.NumberedCountry(1).withId(0).buildBL();

            var mockCountryRepository = new Mock<ICountryRepository>();
            mockCountryRepository.Setup(repo => repo.GetAll()).Returns(_countries);
            var countryService = new CountryService(mockCountryRepository.Object, _mapper);

            // Act
            void action() => countryService.AddCountry(countryBLNew);

            // Assert
            Assert.Throws<CountryAlreadyExistsException>(action);
            mockCountryRepository.Verify(repo => repo.GetAll(), Times.Once);
            mockCountryRepository.Verify(repo => repo.Add(It.IsAny<Country>()), Times.Never);
        }

        /*
        [AllureXunit]
        public void TestCountryDelete() {
            Assert.Equal(0, 1);
        }

        [AllureXunit]
        public void TestCountryUpdate() {
            Assert.Equal(0, 1);
        }

        [AllureXunit]
        public void TestCountryGetById() {
            Assert.Equal(0, 1);
        }

        [AllureXunit]
        public void TestCountryGetAll() {
            Assert.Equal(0, 1);
        }

        [AllureXunit]
        public void TestCountryGetByName() {
            Assert.Equal(0, 1);
        }
        */
    }
}