using ServerING.Models;
using System;
using System.Collections.Generic;

namespace ServerING.Interfaces {
    public interface IPlayerRepository : IRepository<Player> {

        Player GetByNickname(string nickname);
        IEnumerable<Player> GetByHoursPlayed(int hoursPlayed);
        IEnumerable<Player> GetByLastPlayed(DateTime lastPlayed);
    }
}
