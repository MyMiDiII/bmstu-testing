using ServerING.Enums;
using ServerING.Models;

namespace UnitDB {
    public class TestData {
        public Server server = new Server {
            Name = "Server1",
            Ip = "127.0.0.1",
            GameName = "Game",
            Status = ServerStatus.Accepted,
            HostingID = 1,
            PlatformID = 1,
            CountryID = 1,
            OwnerID = 1,
        };

        public Country country = new Country {
            Name = "Country1",
            LevelOfInterest = 1,
            OverallPlayers = 1,
        };

        public Platform platform = new Platform {
            Name = "Platform1",
            Cost = 1,
            Popularity = 1,
        };

        public WebHosting hosting = new WebHosting {
            Name = "Hosting1",
            SubMonths = 1,
            PricePerMonth = 1,
        };

        public User user = new User {
            Login = "User1",
            Password = "1234",
            Role = "admin",
        };

        public Player player = new Player {
            Nickname = "Nickname1",
            HoursPlayed = 1,
            LastPlayed = DateTime.Now,
        };

        public ServerPlayer serverPlayer = new ServerPlayer {
            ServerID = 1,
            PlayerID = 1,
        };

        public FavoriteServer favoriteServer = new FavoriteServer {
            ServerID = 1,
            UserID = 1,
        };
    }
}