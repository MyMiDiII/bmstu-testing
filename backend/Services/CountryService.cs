using System.Collections.Generic;
using System.Linq;
using AutoMapper;

using ServerING.Exceptions;
using ServerING.ModelsBL;
using ServerING.Interfaces;
using ServerING.Models;


namespace ServerING.Services {
    public interface ICountryService {
        CountryBL AddCountry(CountryBL country);
        CountryBL UpdateCountry(int id, CountryBL country);
        CountryBL DeleteCountry(int id);

        CountryBL GetCountryByID(int id);
        IEnumerable<CountryBL> GetAllCountries();

        CountryBL GetCountryByName(string name);
        IEnumerable<CountryBL> GetCountryByOverallPlayers(ushort overallPlayers);
        IEnumerable<CountryBL> GetCountryByLevelOfInterest(int levelOfIntereset);
    }

    public class CountryService : ICountryService {
        private readonly ICountryRepository countryRepository;
        private readonly IMapper mapper;

        public CountryService(ICountryRepository countryRepository, IMapper mapper) {
            this.countryRepository = countryRepository;
            this.mapper = mapper;
        }

        private bool IsExist(CountryBL country) {
            return countryRepository.GetAll()
                .Where(item => item.Id != country.Id)
                .Any(item => item.Name == country.Name);
        }

        private bool IsExistById(int id) {
            return countryRepository.GetByID(id) != null;
        }

        public CountryBL AddCountry(CountryBL country) {
            if (IsExist(country))
                throw new CountryAlreadyExistsException("Country already exists");

            var transferedCountry = mapper.Map<Country>(country);
            return mapper.Map<CountryBL>(countryRepository.Add(transferedCountry));
        }

        public CountryBL DeleteCountry(int id) {
            return mapper.Map<CountryBL>(countryRepository.Delete(id));
        }

        public CountryBL GetCountryByID(int id) {
            return mapper.Map<CountryBL>(countryRepository.GetByID(id));
        }

        public IEnumerable<CountryBL> GetAllCountries() {
            return mapper.Map<IEnumerable<CountryBL>>(countryRepository.GetAll());
        }

        public CountryBL UpdateCountry(int id, CountryBL country) {
            if (!IsExistById(id))
                return null;

            if (IsExist(country))
                throw new CountryAlreadyExistsException("Country already exists");

            var transferedCountry = mapper.Map<Country>(country);
            return mapper.Map<CountryBL>(countryRepository.Update(transferedCountry));
        }

        public CountryBL GetCountryByName(string name) {
            return mapper.Map<CountryBL>(countryRepository.GetByName(name));
        }

        public IEnumerable<CountryBL> GetCountryByOverallPlayers(ushort overallPlayers) {
            return mapper
                .Map<IEnumerable<CountryBL>>(countryRepository.GetByOverallPlayers(overallPlayers));
        }

        public IEnumerable<CountryBL> GetCountryByLevelOfInterest(int levelOfInterest) {
            return mapper
                .Map<IEnumerable<CountryBL>>(countryRepository.GetByLevelOfInterest(levelOfInterest));
        }
    }
}
