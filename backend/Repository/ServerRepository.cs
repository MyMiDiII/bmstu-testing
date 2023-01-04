using AutoMapper;
using ServerING.ModelsBL;
using Microsoft.EntityFrameworkCore;
using ServerING.Interfaces;
using ServerING.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServerING.Repository {
    public class ServerRepository : IServerRepository {

        ///
        private readonly IMapper mapper;
        private readonly AppDBContent appDBContent;

        public ServerRepository(AppDBContent appDBContent, IMapper mapper) {
            this.appDBContent = appDBContent;
            this.mapper = mapper;
        }
        ///

        public Server Add(Server server) {
            try {
                var addedServer = appDBContent.Server.Add(server);
                appDBContent.SaveChanges();

                return GetByID(server.Id);
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("Server Add Error");
            }
        }

        public Server Update(Server server) {
            try {
                var curServer = appDBContent.Server.FirstOrDefault(x => x.Id == server.Id);
                /*appDBContent.Server.Update(curServer);*/
                appDBContent.Entry(curServer).CurrentValues.SetValues(server);
                appDBContent.SaveChanges();

                return GetByID(server.Id);
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("Server Update Error");
            }
        }

        public Server Delete(int id) {
            try {
                Server server = appDBContent.Server.Find(id);

                if (server == null) {
                    return null;
                }
                else {
                    appDBContent.Server.Remove(server);
                    appDBContent.SaveChanges();

                    return server;
                }
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("Server Delete Error");
            }
        }

        public IEnumerable<Server> GetAll() {
            return appDBContent
                .Server
                .Include(s => s.Country)
                .Include(s => s.Platform)
                .Include(s => s.Hosting)
                .Include(s => s.Owner)
                .ToList();
        }

        public Server GetByID(int id) {
            return appDBContent
                .Server
                .Include(s => s.Country)
                .Include(s => s.Platform)
                .Include(s => s.Hosting)
                .Include(s => s.Owner)
                .FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Server> GetByGameName(string gameName) {
            return appDBContent
                .Server
                .Include(s => s.Country)
                .Include(s => s.Platform)
                .Include(s => s.Hosting)
                .Include(s => s.Owner)
                .Where(s => s.GameName == gameName)
                .ToList();
        }

        public Server GetByIP(string ip) {
            return appDBContent
                .Server
                .Include(s => s.Country)
                .Include(s => s.Platform)
                .Include(s => s.Hosting)
                .Include(s => s.Owner)
                .FirstOrDefault(s => s.Ip == ip);
        }

        public Server GetByName(string name) {
            return appDBContent
                .Server
                .Include(s => s.Country)
                .Include(s => s.Platform)
                .Include(s => s.Hosting)
                .Include(s => s.Owner)
                .FirstOrDefault(s => s.Name == name);
        }

        public IEnumerable<Server> GetByPlatformID(int id) {
            return appDBContent
                .Server
                .Include(s => s.Country)
                .Include(s => s.Platform)
                .Include(s => s.Hosting)
                .Include(s => s.Owner)
                .Where(s => s.PlatformID == id)
                .ToList();
        }

        public IEnumerable<Server> GetByWebHostingID(int id) {
            return appDBContent
                .Server
                .Include(s => s.Country)
                .Include(s => s.Platform)
                .Include(s => s.Hosting)
                .Include(s => s.Owner)
                .Where(s => s.HostingID == id).ToList();
        }

        public IEnumerable<Player> GetPlayersByServerID(int id) {
            Server server = appDBContent
                .Server
                .Include(s => s.Country)
                .Include(s => s.Platform)
                .Include(s => s.Hosting)
                .Include(s => s.Owner)
                .FirstOrDefault(s => s.Id == id);

            if (server != null) {
                var playersOnServerIds = appDBContent.ServerPlayer.Where(x => x.ServerID == id).Select(x => x.PlayerID).ToList();

                IEnumerable<Player> players = appDBContent.Player.Where(x => playersOnServerIds.Contains(x.Id)).ToList();

                return players;
            }

            return null;
        }

        public WebHosting GetWebHostingByServerId(int id) {

            if (id > 0) {
                Server server = appDBContent
                    .Server
                    .Include(s => s.Country)
                    .Include(s => s.Platform)
                    .Include(s => s.Hosting)
                    .Include(s => s.Owner)
                    .FirstOrDefault(s => s.Id == id);

                if (server != null) {
                    return appDBContent.WebHosting.FirstOrDefault(w => w.Id == server.HostingID);
                }
            }

            return null;
        }

        public IEnumerable<Server> GetByRating(int rating) {
            return appDBContent
                .Server
                .Include(s => s.Country)
                .Include(s => s.Platform)
                .Include(s => s.Hosting)
                .Include(s => s.Owner)
                .Where(x => x.Rating == rating)
                .ToList();
        }

        public IEnumerable<FavoriteServer> GetByUserID(int id) {
            return appDBContent.FavoriteServer.Where(fs => fs.UserID == id).ToList();
        }

        public Country GetCountryByServerId(int id) {
            if (id > 0) {
                Server server = appDBContent.Server.FirstOrDefault(s => s.Id == id);

                if (server != null) {
                    return appDBContent.Country.FirstOrDefault(c => c.Id == server.CountryID);
                }
            }

            return null;
        }
    }
}
