using Microsoft.EntityFrameworkCore;
using ServerING.Interfaces;
using ServerING.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using ServerING.Exceptions;

namespace ServerING.Repository {
    public class UserRepository : IUserRepository {

        ///
        private readonly AppDBContent appDBContent;

        public UserRepository(AppDBContent appDBContent) {
            this.appDBContent = appDBContent;
        }
        ///

        public User Add(User user) {
            try {
                appDBContent.User.Add(user);
                appDBContent.SaveChanges();

                return GetByID(user.Id);
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("User Add Error");
            }
        }

        public User Delete(int id) {
            try {
                User user = appDBContent.User.Find(id);

                if (user == null) {
                    return null;
                }
                else {
                    appDBContent.User.Remove(user);
                    appDBContent.SaveChanges();

                    return user;
                }
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("User Delete Error");
            }
        }

        public User Update(User user) {
            try {
                var curUser = appDBContent.User.FirstOrDefault(x => x.Id == user.Id);
                appDBContent.Entry(curUser).CurrentValues.SetValues(user);
                /*appDBContent.User.Update(user);*/
                appDBContent.SaveChanges();

                return GetByID(user.Id);
            }
            catch (Exception ex) {
                throw new UserException(ex.InnerException.Message);
            }
        }

        public IEnumerable<User> GetAll() {
            return appDBContent.User.ToList();
        }

        public User GetByID(int id) {
            return appDBContent.User.Find(id);
        }

        public User GetByLogin(string login) {
            return appDBContent.User.FirstOrDefault(u => u.Login == login);
        }

        public IEnumerable<User> GetByRole(string role) {
            return appDBContent.User.Where(u => u.Role == role).ToList();
        }

        public IEnumerable<Server> GetFavoriteServersByUserId(int id) {

            var favServs = appDBContent.FavoriteServer
                .Where(x => x.UserID == id)
                .Select(x => x.ServerID).ToList();

            IEnumerable<Server> servers = appDBContent.Server
                .Where(x => favServs.Contains(x.Id)).ToList();

            return servers;
        }

        public FavoriteServer AddFavoriteServer(FavoriteServer favoriteServer) {
            try {
                appDBContent.FavoriteServer.Add(favoriteServer);
                appDBContent.SaveChanges();

                return favoriteServer;
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("FavoriteServer Add Error");
            }
        }

        public FavoriteServer DeleteFavoriteServer(int id) {

            try {
                FavoriteServer favoriteServer = appDBContent.FavoriteServer.Find(id);

                if (favoriteServer == null) {
                    return null;
                }
                else {
                    appDBContent.FavoriteServer.Remove(favoriteServer);
                    appDBContent.SaveChanges();

                    return favoriteServer;
                }
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("FavoriteServer Delete Error");
            }
        }

        public FavoriteServer GetFavoriteServerByUserAndServerId(int userId, int serverId) {
            return appDBContent.FavoriteServer.FirstOrDefault(fs => fs.UserID == userId && fs.ServerID == serverId);
        }
    }
}
