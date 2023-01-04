using ServerING.Interfaces;
using ServerING.Models;
using System.Collections.Generic;
using System.Linq;


namespace ServerING.Mocks {
    /*
    public class ServerMock : IServerRepository {

        private List<Server> _servers = new List<Server> {
            new Server {
                Id = 1,
                Name = "Server1",
                Ip = "IP1",
                GameVersion = "GV1",
                HostingID = 1,
                PlatformID = 1
            },
            new Server {
                Id = 2,
                Name = "Server2",
                Ip = "IP2",
                GameVersion = "GV2",
                HostingID = 2,
                PlatformID = 2
            },
            new Server {
                Id = 3,
                Name = "Server3",
                Ip = "IP3",
                GameVersion = "GV3",
                HostingID = 3,
                PlatformID = 3
            }
        };


        public void Add(Server model) {
            _servers.Add(model);
        }

        public Server Delete(int id) {
            Server server = _servers[id - 1];
            _servers.Remove(server);

            return server;
        }

        public IEnumerable<Server> GetAll() {
            return _servers;
        }

        public IEnumerable<Server> GetByGameVersion(string gameVersion) {
            return _servers.Where(x => x.GameVersion == gameVersion);
        }

        public Server GetByID(int id) {
            return _servers[id - 1];
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

        public IEnumerable<Server> GetByWebHostingID(int id) {
            return _servers.Where(x => x.HostingID == id);
        }

        public void Update(Server model) {
            Server server = _servers[model.Id - 1];

            server.Name = model.Name;
            server.Ip = model.Ip;
            server.GameVersion = model.GameVersion;
            server.PlatformID = model.PlatformID;
            server.HostingID = model.HostingID;

            _servers[model.Id - 1] = server;
        }
    }
    */
}
