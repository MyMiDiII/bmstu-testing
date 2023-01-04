using ServerING.Models;

namespace ServerING.Interfaces {
    public interface ICountryRepository : IRepository<Country> {
        Country GetByName(string name);
    }
}
