using ServerING.Models;
using System.Collections.Generic;


namespace ServerING.Interfaces {
    public interface IPlatformRepository : IRepository<Platform> {

        Platform GetByName(string name);
        IEnumerable<Platform> GetByPopularity(ushort popularity);
        IEnumerable<Platform> GetByCost(int cost);
    }
}
