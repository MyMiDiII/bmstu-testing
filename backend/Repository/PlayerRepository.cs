using ServerING.Interfaces;
using ServerING.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServerING.Repository {
    public class PlayerRepository : IPlayerRepository {

        ///
        private readonly AppDBContent appDBContent;

        public PlayerRepository(AppDBContent appDBContent) {
            this.appDBContent = appDBContent;
        }
        ///


        public Player Add(Player player) {
            try {
                appDBContent.Player.Add(player);
                appDBContent.SaveChanges();

                return GetByID(player.Id);
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("Player Add Error");
            }
        }

        public Player Delete(int id) {
            try {
                Player player = appDBContent.Player.Find(id);

                if (player == null) {
                    return null;
                }
                else {
                    appDBContent.Player.Remove(player);
                    appDBContent.SaveChanges();

                    return player;
                }
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("Player Delete Error");
            }
        }


        public Player Update(Player player) {
            try {
                var curPlayer = appDBContent.Player.FirstOrDefault(x => x.Id == player.Id);
                appDBContent.Entry(curPlayer).CurrentValues.SetValues(player);
                /*appDBContent.Player.Update(player);*/
                appDBContent.SaveChanges();

                return GetByID(player.Id);
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("Player Update Error");
            }
        }

        public IEnumerable<Player> GetAll() {
            return appDBContent.Player.ToList();
        }

        public Player GetByID(int id) {
            return appDBContent.Player.Find(id);
        }

        public IEnumerable<Player> GetByHoursPlayed(int hoursPlayed) {
            return appDBContent.Player.Where(p => p.HoursPlayed == hoursPlayed).ToList();
        }

        public IEnumerable<Player> GetByLastPlayed(DateTime lastPlayed) {
            return appDBContent.Player.Where(p => p.LastPlayed == lastPlayed).ToList();
        }

        public Player GetByNickname(string nickname) {
            return appDBContent.Player.FirstOrDefault(p => p.Nickname == nickname);
        }
    }
}
