using ServerING.Interfaces;
using ServerING.Models;


namespace UnitBL {
    /*
    public class PlayerMock : MockData, IPlayerRepository {

        public void Add(Player model) {
            _players.Add(model);
        }

        public Player Delete(int id) {
            Player player = _players[id - 1];
            _players.Remove(player);

            return player;
        }

        public IEnumerable<Player> GetAll() {
            return _players;
        }

        public IEnumerable<Player> GetByHoursPlayed(int hoursPlayed) {
            return _players.Where(x => x.HoursPlayed == hoursPlayed);
        }

        public Player GetByID(int id) {
            return _players[id - 1];
        }

        public IEnumerable<Player> GetByLastPlayed(DateTime lastPlayed) {
            return _players.Where(x =>x.LastPlayed == lastPlayed);
        }

        public Player GetByNickname(string nickname) {
            return _players.FirstOrDefault(x => x.Nickname == nickname);
        }

        public void Update(Player model) {
            Player player = _players[model.Id - 1];

            player.Nickname = model.Nickname;
            player.HoursPlayed = model.HoursPlayed;
            player.LastPlayed = model.LastPlayed;

            _players[model.Id - 1] = player;
        }
    }
    */
}
