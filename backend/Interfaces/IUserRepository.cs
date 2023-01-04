using ServerING.Models;
using System.Collections.Generic;

namespace ServerING.Interfaces {
    public interface IUserRepository : IRepository<User> {

        User GetByLogin(string login);
        IEnumerable<User> GetByRole(string role);

        IEnumerable<Server> GetFavoriteServersByUserId(int id);
        FavoriteServer AddFavoriteServer(FavoriteServer favoriteServer);
        FavoriteServer DeleteFavoriteServer(int id);
        FavoriteServer GetFavoriteServerByUserAndServerId(int userId, int serverId);
    }
}
