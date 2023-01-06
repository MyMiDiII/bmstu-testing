using ServerING.Interfaces;
using ServerING.Models;


namespace UnitBL {
    /*
    public class ServerStub : StubData, IServerRepository {

        public void Add(Server model) {
            model.Id = _servers.Count + 1;
            _servers.Add(model);
        }

        public Server Delete(int id) {
            Server server = _servers.FirstOrDefault(x => x.Id == id);

            _servers.Remove(server);

            IEnumerable<int> favoriteServersIds = _favoriteServers.Where(x => x.ServerID == id).Select(x => x.Id);
            foreach (int favoriteServerId in favoriteServersIds.ToList()) { 
                FavoriteServer favoriteServer = _favoriteServers.FirstOrDefault(x => x.Id == favoriteServerId);
                _favoriteServers.Remove(favoriteServer);
            }

            IEnumerable<int> serverPlayersIds = _serverPlayers.Where(x => x.ServerID == id).Select(x => x.Id);
            foreach (int serverPlayersId in serverPlayersIds.ToList()) {
                ServerPlayer serverPlayer = _serverPlayers.FirstOrDefault(x => x.Id == serverPlayersId);
                _serverPlayers.Remove(serverPlayer);
            }

            return server;
        }

        public IEnumerable<Server> GetAll() {
            return _servers;
        }

        public IEnumerable<Server> GetByGameVersion(string gameVersion) {
            return _servers.Where(x => x.GameVersion == gameVersion);
        }

        public Server GetByID(int id) {
            return _servers.FirstOrDefault(x => x.Id == id);
        }

        public Server GetByIP(string ip) {
            return _servers.FirstOrDefault(x => x.Ip == ip);
        }

        public Server GetByName(string name) {
            return _servers.FirstOrDefault(x => x.Name == name);
        }

        public IEnumerable<Server> GetByPlatformID(int id) {
            return _servers.Where(x => x.PlatformID == id);
        }

        public IEnumerable<Server> GetByRating(int rating) {
            return _servers.Where(x => x.Rating == rating);
        }

        public IEnumerable<FavoriteServer> GetByUserID(int id) {
            return _favoriteServers.Where(fs => fs.UserID == id);
        }

        public IEnumerable<Server> GetByWebHostingID(int id) {
            return _servers.Where(x => x.HostingID == id);
        }

        public IEnumerable<Player> GetPlayersByServerID(int id) {
            Server server = _servers.FirstOrDefault(s => s.Id == id);

            if (server != null) {
                var playersOnServerIds = _serverPlayers.Where(x => x.ServerID == id).Select(x => x.PlayerID);

                IEnumerable<Player> players = _players.Where(x => playersOnServerIds.Contains(x.Id));

                return players;
            }

            return null;
        }

        public WebHosting GetWebHostingByServerId(int id) {

            if (id > 0) {
                Server server = _servers.FirstOrDefault(s => s.Id == id);

                if (server != null) {
                    return _webHostings.FirstOrDefault(w => w.Id == server.HostingID);
                }
            }

            return null;
        }

        public void Update(Server model) {
            Server server = _servers.FirstOrDefault(s => s.Id == model.Id);
            int id = _servers.FindIndex(s => s.Name == server.Name && s.Ip == server.Ip);

            server.Name = model.Name;
            server.Ip = model.Ip;
            server.GameVersion = model.GameVersion;
            server.PlatformID = model.PlatformID;
            server.HostingID = model.HostingID;

            _servers[id] = server;
        }
    }
    */
}
