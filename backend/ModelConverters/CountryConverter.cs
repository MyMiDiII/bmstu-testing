using ServerING.DTO;
using ServerING.ModelsBL;
using ServerING.Services;

namespace ServerING.ModelConverters {
    public class CountryConverters {

        private readonly ICountryService countryService;

        public CountryConverters(ICountryService countryService) {
            this.countryService = countryService;
        }

        public CountryBL convertPatch(int id, CountryBaseDto country) {
            var existedCountry = countryService.GetCountryByID(id);

            return new CountryBL {
                Id = id,
                Name = country.Name ?? existedCountry.Name,
                LevelOfInterest = country.LevelOfInterest ?? existedCountry.LevelOfInterest,
                OverallPlayers = country.OverallPlayers ?? existedCountry.OverallPlayers
            };
        }
    }
}