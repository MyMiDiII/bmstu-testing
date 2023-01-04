using ServerING.Interfaces;
using ServerING.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServerING.Repository {
    public class CountryRepository : ICountryRepository {

        ///
        private readonly AppDBContent appDBContent;

        public CountryRepository(AppDBContent appDBContent) {
            this.appDBContent = appDBContent;
        }
        ///

        public Country Add(Country country) {
            try {
                appDBContent.Country.Add(country);
                appDBContent.SaveChanges();

                return GetByID(country.Id);
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("Country Add Error");
            }
        }

        public Country Delete(int id) {
            try {
                Country country = appDBContent.Country.Find(id);

                if (country == null) {
                    return null;
                }
                else {
                    appDBContent.Country.Remove(country);
                    appDBContent.SaveChanges();

                    return country;
                }
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("Country Delete Error");
            }
        }

        public IEnumerable<Country> GetAll() {
            return appDBContent.Country.ToList();
        }

        public Country GetByID(int id) {
            return appDBContent.Country.Find(id);
        }

        public IEnumerable<Country> GetByLevelOfInterest(int levelOfInterest) {
            return appDBContent.Country.Where(c => c.LevelOfInterest == levelOfInterest).ToList();
        }

        public Country GetByName(string name) {
            return appDBContent.Country.FirstOrDefault(c => c.Name == name);
        }

        public IEnumerable<Country> GetByOverallPlayers(int overallPlayers) {
            return appDBContent.Country.Where(c => c.OverallPlayers == overallPlayers).ToList();
        }

        public Country Update(Country country) {
            try {
                var curCountry = appDBContent.Country.FirstOrDefault(x => x.Id == country.Id);
                appDBContent.Entry(curCountry).CurrentValues.SetValues(country);
                appDBContent.SaveChanges();

                return GetByID(country.Id);
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("Country Update Error");
            }
        }
    }
}
