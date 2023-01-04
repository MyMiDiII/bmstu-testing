using ServerING.Models;
using System.Collections.Generic;

namespace ServerING.Interfaces {
    public interface IServerRepository : IRepository<Server> {
        Server GetByName(string name);
        Server GetByIP(string ip);

        IEnumerable<Player> GetPlayersByServerID(int id);
    }
}
