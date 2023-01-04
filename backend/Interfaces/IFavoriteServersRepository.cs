using ServerING.Models;
using System.Collections.Generic;

namespace ServerING.Interfaces {
    public interface IFavoriteServerRepository : IRepository<FavoriteServer> {

        IEnumerable<FavoriteServer> GetByServerID(int id);
        IEnumerable<FavoriteServer> GetByUserID(int id);
    }
}
