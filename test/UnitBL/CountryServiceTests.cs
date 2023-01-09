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

        [AllureXunit]
        public void TestCountryDelete() {
            // Arrange
            var mockCountryRepository = new Mock<ICountryRepository>();
            var countryService = new CountryService(mockCountryRepository.Object, _mapper);

            // Act
            countryService.DeleteCountry(1);

            // Assert
            mockCountryRepository.Verify(repo => repo.Delete(1), Times.Once);
        }

        [AllureXunit]
        public void TestCountryUpdate() {
            // Arrange
            var countryBLNew = CountriesOM.NumberedCountry(3).withId(0).buildBL();

            var mockCountryRepository = new Mock<ICountryRepository>();
            mockCountryRepository.Setup(repo => repo.GetByID(1)).Returns(
                _countries.First(item => item.Id == 1));
            mockCountryRepository.Setup(repo => repo.GetAll()).Returns(_countries);
            var countryService = new CountryService(mockCountryRepository.Object, _mapper);

            // Act
            countryService.UpdateCountry(1, countryBLNew);

            // Assert
            mockCountryRepository.Verify(repo => repo.GetByID(2), Times.Once);
            mockCountryRepository.Verify(repo => repo.GetAll(), Times.Once);
            mockCountryRepository.Verify(repo => repo.Update(It.IsAny<Country>()), Times.Once);
        }

        [AllureXunit]
        public void TestCountryGetById() {
            var mockCountryRepository = new Mock<ICountryRepository>();
            mockCountryRepository.Setup(repo => repo.GetByID(1)).Returns(
                _countries.First(item => item.Id == 1));
            var countryService = new CountryService(mockCountryRepository.Object, _mapper);

            // Act
            countryService.GetCountryByID(1);

            // Assert
            mockCountryRepository.Verify(repo => repo.GetByID(1), Times.Once);
        }

        [AllureXunit]
        public void TestCountryGetAll() {
            // Arrange
            var mockCountryRepository = new Mock<ICountryRepository>();
            mockCountryRepository.Setup(repo => repo.GetAll()).Returns(_countries);
            var countryService = new CountryService(mockCountryRepository.Object, _mapper);

            // Act
            countryService.GetAllCountries();

            // Assert
            mockCountryRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        [AllureXunit]
        public void TestCountryGetByName() {
            var mockCountryRepository = new Mock<ICountryRepository>();
            mockCountryRepository.Setup(repo => repo.GetByName("C1")).Returns(
                _countries.First(item => item.Name == "C1"));
            var countryService = new CountryService(mockCountryRepository.Object, _mapper);

            // Act
            countryService.GetCountryByName("C1");

            // Assert
            mockCountryRepository.Verify(repo => repo.GetByName("C1"), Times.Once);
        }
    }
}