using ServerING.Models;

namespace ServerING.Interfaces {
    public interface IPlayerRepository : IRepository<Player> {
        Player GetByNickname(string nickname);
    }
}
