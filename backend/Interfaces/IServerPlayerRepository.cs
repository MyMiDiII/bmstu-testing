using ServerING.Models;
using System.Collections.Generic;

namespace ServerING.Interfaces {
    public interface IServerPlayerRepository : IRepository<ServerPlayer> {

        IEnumerable<ServerPlayer> GetByServerID(int id);
        IEnumerable<ServerPlayer> GetByPlayerID(int id);
    }
}
