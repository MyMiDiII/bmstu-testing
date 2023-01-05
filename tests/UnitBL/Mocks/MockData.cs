using ServerING.Models;
using System;
using System.Collections.Generic;

namespace UnitBL {
    public class MockData {

        static public List<FavoriteServer> _favoriteServers = new List<FavoriteServer>();

        static public List<Server> _servers = new List<Server> {
            new Server {
                Id = 1,
                Name = "Server1",
                Ip = "IP1",
                HostingID = 1,
                PlatformID = 1
            },
            new Server {
                Id = 2,
                Name = "Server2",
                Ip = "IP2",
                HostingID = 2,
                PlatformID = 2
            },
            new Server {
                Id = 3,
                Name = "Server3",
                Ip = "IP3",
                HostingID = 3,
                PlatformID = 3
            },
            new Server {
                Id = 4,
                Name = "Server4",
                Ip = "IP4",
                HostingID = 4,
                PlatformID = 1
            },
            new Server {
                Id = 5,
                Name = "Server5",
                Ip = "IP5",
                HostingID = 5,
                PlatformID = 2
            },
            new Server {
                Id = 6,
                Name = "Server6",
                Ip = "IP6",
                HostingID = 6,
                PlatformID = 3
            }
        };

        static public List<Platform> _platforms = new List<Platform> {
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

        static public List<WebHosting> _webHostings = new List<WebHosting> {
            new WebHosting {
                Id = 1,
                Name = "WH1",
                PricePerMonth = 1000,
                SubMonths = 1
            },
            new WebHosting {
                Id = 2,
                Name = "WH2",
                PricePerMonth = 2000,
                SubMonths = 2
            },
            new WebHosting {
                Id = 3,
                Name = "WH3",
                PricePerMonth = 3000,
                SubMonths = 3
            }
        };

        static public List<Player> _players = new List<Player> {
            new Player {
                Id = 1,
                Nickname = "NN1",
                HoursPlayed = 1,
                LastPlayed = new DateTime(2022, 5, 5)
            },
            new Player {
                Id = 2,
                Nickname = "NN2",
                HoursPlayed = 2,
                LastPlayed = new DateTime(2022, 5, 6)
            },
            new Player {
                Id = 3,
                Nickname = "NN3",
                HoursPlayed = 3,
                LastPlayed = new DateTime(2022, 5, 7)
            }
        };

        static public List<ServerPlayer> _serverPlayers = new List<ServerPlayer> {
            new ServerPlayer {
                Id = 1,
                PlayerID = 1,
                ServerID = 1
            },
            new ServerPlayer {
                Id = 2,
                PlayerID = 2,
                ServerID = 1
            },
            new ServerPlayer {
                Id = 3,
                PlayerID = 3,
                ServerID = 1
            },
            new ServerPlayer {
                Id = 4,
                PlayerID = 1,
                ServerID = 2
            },
            new ServerPlayer {
                Id = 5,
                PlayerID = 2,
                ServerID = 2
            },
            new ServerPlayer {
                Id = 6,
                PlayerID = 3,
                ServerID = 2
            }
        };

        static public List<User> _users = new List<User>() {
            new User {
                Id = 1,
                Login = "Login1",
                Password = "12345",
                Role = "User1"
            },
            new User {
                Id = 2,
                Login = "Login2",
                Password = "12345",
                Role = "User2"
            },
            new User {
                Id = 3,
                Login = "Login3",
                Password = "12345",
                Role = "User3"
            },
            new User {
                Id = 4,
                Login = "admin",
                Password = "1234",
                Role = "Admin"
            },
            new User {
                Id = 5,
                Login = "user",
                Password = "1234",
                Role = "User"
            }
        };
    }
}
