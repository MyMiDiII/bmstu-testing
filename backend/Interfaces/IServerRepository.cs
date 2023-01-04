using ServerING.Models;
using System.Collections.Generic;

namespace ServerING.Interfaces {
    public interface IServerRepository : IRepository<Server> {
        Server GetByName(string name);
        Server GetByIP(string ip);
        IEnumerable<Server> GetByGameName(string gameName);

        IEnumerable<Server> GetByWebHostingID(int id);
        IEnumerable<Server> GetByPlatformID(int id);
        IEnumerable<Server> GetByRating(int rating);

        IEnumerable<Player> GetPlayersByServerID(int id);
        WebHosting GetWebHostingByServerId(int id);
        Country GetCountryByServerId(int id);

        public IEnumerable<FavoriteServer> GetByUserID(int id);
    }
}
