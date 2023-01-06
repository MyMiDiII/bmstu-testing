using ServerING.Interfaces;
using ServerING.Models;


namespace UnitBL {
    public class PlatformStub : IPlatformRepository {
        private List<Platform> _platforms = new List<Platform> {
            new Platform {
                Id = 1,
                Name = "Platform1",
                Popularity = 1,
                Cost = 1000
            },
            new Platform {
                Id = 2,
                Name = "Platform2",
                Popularity = 2,
                Cost = 2000
            },
            new Platform {
                Id = 3,
                Name = "Platform3",
                Popularity = 3,
                Cost = 3000
            }
        };
        private int _nextID = 4;

        public Platform Add(Platform model) {
            model.Id  = _nextID;

            _nextID++;
            _platforms.Add(model);

            return _platforms.Last();
        }

        public Platform Delete(int id) {
            Platform platform = _platforms.First(x => x.Id == id);
            _platforms.Remove(platform);

            return platform;
        }

        public IEnumerable<Platform> GetAll() {
            return _platforms;
        }

        public Platform GetByID(int id) {
            return _platforms.First(x => x.Id == id);
        }

        public Platform GetByName(string name) {
            return _platforms.FirstOrDefault(x => x.Name == name) ?? new Platform();
        }

        public Platform Update(Platform model) {
            Platform plat = _platforms[model.Id - 1];

            plat.Id = model.Id;
            plat.Name = model.Name;
            plat.Popularity = model.Popularity;
            plat.Cost = model.Cost;

            _platforms[model.Id - 1] = plat;

            return plat;
        }
    }
}
