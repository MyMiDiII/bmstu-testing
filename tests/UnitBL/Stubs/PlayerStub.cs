using ServerING.Interfaces;
using ServerING.Models;


namespace UnitBL {
    public class PlayerStub : IPlayerRepository {
        private List<Player> _players = new List<Player> {
            new Player {
                Id = 1,
                Nickname = "NN1",
                HoursPlayed = 1,
                LastPlayed = new DateTime(2022, 5, 5)
            },
            new Player {
                Id = 2,
                Nickname = "NN2",
                HoursPlayed = 2,
                LastPlayed = new DateTime(2022, 5, 6)
            },
            new Player {
                Id = 3,
                Nickname = "NN3",
                HoursPlayed = 3,
                LastPlayed = new DateTime(2022, 5, 7)
            }
        };
        private int _nextID = 4;

        public Player Add(Player model) {
            model.Id  = _nextID;

            _nextID++;
            _players.Add(model);

            return _players.Last();
        }

        public Player Delete(int id) {
            Player player = _players.First(x => x.Id == id);
            _players.Remove(player);

            return player;
        }

        public IEnumerable<Player> GetAll() {
            return _players;
        }

        public Player GetByID(int id) {
            return _players.First(x => x.Id == id);
        }

        public Player GetByNickname(string name) {
            return _players.FirstOrDefault(x => x.Nickname == name) ?? new Player();
        }

        public Player Update(Player model) {
            Player player = _players[model.Id - 1];

            player.Id = model.Id;
            player.Nickname = model.Nickname;
            player.HoursPlayed = model.HoursPlayed;
            player.LastPlayed = model.LastPlayed;

            _players[model.Id - 1] = player;

            return player;
        }
    }

}
