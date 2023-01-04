using ServerING.Models;
using System.Collections.Generic;


namespace ServerING.Interfaces {
    public interface ICountryRepository : IRepository<Country> {

        Country GetByName(string name);
        IEnumerable<Country> GetByLevelOfInterest(int levelOfInterest);
        IEnumerable<Country> GetByOverallPlayers(int overallPlayers);
    }
}
