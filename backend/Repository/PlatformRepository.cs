using ServerING.Interfaces;
using ServerING.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServerING.Repository {
    public class PlatformRepository : IPlatformRepository {

        ///
        private readonly AppDBContent appDBContent;

        public PlatformRepository(AppDBContent appDBContent) {
            this.appDBContent = appDBContent;
        }
        ///

        public Platform Add(Platform platform) {
            try {
                appDBContent.Platform.Add(platform);
                appDBContent.SaveChanges();

                return GetByID(platform.Id);
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("Platform Add Error");
            }
        }

        public Platform Delete(int id) {
            try {
                Platform platform = appDBContent.Platform.Find(id);

                if (platform == null) {
                    return null;
                }
                else {
                    appDBContent.Platform.Remove(platform);
                    appDBContent.SaveChanges();

                    return platform;
                }
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("Platform Delete Error");
            }
        }

        public Platform Update(Platform platform) {
            try {
                var curPlatform = appDBContent.Platform.FirstOrDefault(x => x.Id == platform.Id);
                appDBContent.Entry(curPlatform).CurrentValues.SetValues(platform);
                /*appDBContent.Platform.Update(platform);*/
                appDBContent.SaveChanges();

                return GetByID(platform.Id);
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("Platform Update Error");
            }
        }

        public IEnumerable<Platform> GetAll() {
            return appDBContent.Platform.ToList();
        }

        public IEnumerable<Platform> GetByCost(int cost) {
            return appDBContent.Platform.Where(p => p.Cost == cost).ToList();
        }

        public Platform GetByID(int id) {
            return appDBContent.Platform.Find(id);
        }

        public Platform GetByName(string name) {
            return appDBContent.Platform.FirstOrDefault(p => p.Name == name);
        }

        public IEnumerable<Platform> GetByPopularity(ushort popularity) {
            return appDBContent.Platform.Where(p => p.Popularity == popularity).ToList();
        }
    }
}
