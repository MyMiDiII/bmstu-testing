using ServerING.Models;
using ServerING.ModelsBL;

namespace Builders
{
    public class CountryBuilder {
        private int id;
        private string name = "";
        private int levelOfInterest;
        private int overallPlayers;

        public CountryBuilder withId(int id) {
            this.id = id;
            return this;
        }

        public CountryBuilder withName(string name) {
            this.name = name; 
            return this;
        }

        public CountryBuilder withInterestLevel(int level) {
            this.levelOfInterest = level;
            return this;
        }

        public CountryBuilder withPlayers(int players) {
            this.overallPlayers = players;
            return this;
        }

        public Country build() {
            var country = new Country() {
                Id = id,
                Name = name,
                LevelOfInterest = levelOfInterest,
                OverallPlayers = overallPlayers
            };
            return country;
        }

        public CountryBL buildBL() {
            var country = new CountryBL() {
                Id = id,
                Name = name,
                LevelOfInterest = levelOfInterest,
                OverallPlayers = overallPlayers
            };
            return country;
        }
    }
}