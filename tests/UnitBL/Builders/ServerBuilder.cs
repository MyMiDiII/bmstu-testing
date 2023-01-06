using ServerING.Models;
using ServerING.ModelsBL;
using ServerING.Enums;

namespace Builders
{
    public class ServerBuilder {
        private int id;
        private string name = "";
        private string ip = "";
        private string game = "";
        private int rating;
        private ServerStatus status;
        private int hostingID;
        private int platformID;
        private int countryID;
        private int ownerID;

        public ServerBuilder withId(int id) {
            this.id = id;
            return this;
        }

        public ServerBuilder withName(string name) {
            this.name = name; 
            return this;
        }

        public ServerBuilder withIp(string ip) {
            this.ip = ip; 
            return this;
        }

        public ServerBuilder withGame(string game) {
            this.game = game; 
            return this;
        }

        public ServerBuilder withRating(int rating) {
            this.rating = rating;
            return this;
        }

        public ServerBuilder withStatus(ServerStatus status) {
            this.status= status;
            return this;
        }

        public ServerBuilder withHosting(int hostingID) {
            this.hostingID = hostingID;
            return this;
        }

        public ServerBuilder withPlatform(int platformID) {
            this.platformID = platformID;
            return this;
        }

        public ServerBuilder withCountry(int countryID) {
            this.countryID = countryID;
            return this;
        }

        public ServerBuilder withOwner(int ownerID) {
            this.ownerID = ownerID;
            return this;
        }

        public Server build() {
            var server = new Server() {
                Id = id,
                Name = name,
                Ip = ip,
                GameName = game,
                Rating = rating,
                Status = status,
                HostingID = hostingID,
                PlatformID = platformID,
                CountryID = countryID,
                OwnerID = ownerID
            };
            return server;
        }

        public ServerBL buildBL() {
            var server = new ServerBL() {
                Id = id,
                Name = name,
                Ip = ip,
                GameName = game,
                Rating = rating,
                Status = status,
                HostingID = hostingID,
                PlatformID = platformID,
                CountryID = countryID,
                OwnerID = ownerID
            };
            return server;
        }
    }
}