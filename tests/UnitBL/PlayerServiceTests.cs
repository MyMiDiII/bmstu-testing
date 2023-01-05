using AutoMapper;

using ServerING.Utils;
using ServerING.Services;
using ServerING.ModelsBL;
using ServerING.Interfaces;

namespace UnitBL
{
    public class PlayerServiceTests 
    {
        private IMapper _mapper;

        public PlayerServiceTests() {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMappingProfile());
            });
            _mapper = mockMapper.CreateMapper();
        }

        [Fact]
        public void TestPlayerAdd() {
            IPlayerRepository playerRepository = new PlayerMock();
            PlayerService playerService = new PlayerService(playerRepository, _mapper);

            PlayerBL expectedPlayer = new PlayerBL {
                Id = 4,
                Nickname = "NN4",
                HoursPlayed = 4,
                LastPlayed = new DateTime(2022, 5, 5)
            };

            PlayerBL actualPlayer = playerService.AddPlayer(expectedPlayer);

            Assert.NotEqual(0, actualPlayer.Id);
            Assert.Equal(expectedPlayer.Nickname, actualPlayer.Nickname);
            Assert.Equal(expectedPlayer.HoursPlayed, actualPlayer.HoursPlayed);
            Assert.Equal(expectedPlayer.LastPlayed, actualPlayer.LastPlayed);

        }

        [Fact]
        public void TestPlayerDelete() {
            IPlayerRepository playerRepository = new PlayerMock();
            PlayerService playerService = new PlayerService(playerRepository, _mapper);

            PlayerBL expectedPlayer = new PlayerBL {
                Id = 3,
                Nickname = "NN3",
                HoursPlayed = 3,
                LastPlayed = new DateTime(2022, 5, 7)
            };

            PlayerBL actualPlayer = playerService.DeletePlayer(3);

            Assert.Equal(expectedPlayer.Id, actualPlayer.Id);
            Assert.Equal(expectedPlayer.Nickname, actualPlayer.Nickname);
            Assert.Equal(expectedPlayer.HoursPlayed, actualPlayer.HoursPlayed);
            Assert.Equal(expectedPlayer.LastPlayed, actualPlayer.LastPlayed);
        }

        [Fact]
        public void TestPlayerUpdate() {
            IPlayerRepository playerRepository = new PlayerMock();
            PlayerService playerService = new PlayerService(playerRepository, _mapper);

            PlayerBL expectedPlayer = new PlayerBL {
                Id = 1,
                Nickname = "NN1Changed",
                HoursPlayed = 1,
                LastPlayed = new DateTime(2022, 5, 5)
            };

            PlayerBL actualPlayer = playerService.UpdatePlayer(1, expectedPlayer);

            Assert.Equal(expectedPlayer.Nickname, actualPlayer.Nickname);
            Assert.Equal(expectedPlayer.HoursPlayed, actualPlayer.HoursPlayed);
            Assert.Equal(expectedPlayer.LastPlayed, actualPlayer.LastPlayed);
        }

        [Fact]
        public void TestPlayerGetById() {
            IPlayerRepository playerRepository = new PlayerMock();
            PlayerService playerService = new PlayerService(playerRepository, _mapper);

            PlayerBL expectedPlayer = new PlayerBL {
                Id = 1,
                Nickname = "NN1",
                HoursPlayed = 1,
                LastPlayed = new DateTime(2022, 5, 5)
            };

            PlayerBL actualPlayer = playerService.GetPlayerByID(1);

            Assert.Equal(expectedPlayer.Id, actualPlayer.Id);
            Assert.Equal(expectedPlayer.Nickname, actualPlayer.Nickname);
            Assert.Equal(expectedPlayer.HoursPlayed, actualPlayer.HoursPlayed);
            Assert.Equal(expectedPlayer.LastPlayed, actualPlayer.LastPlayed);
        }

        [Fact]
        public void TestPlayerGetAll() {
            IPlayerRepository playerRepository = new PlayerMock();
            PlayerService playerService = new PlayerService(playerRepository, _mapper);

            var players = playerService.GetAllPlayers();

            Assert.IsType<List<PlayerBL>>(players);
            Assert.Equal(3, players.Count());
            Assert.All(players, item => Assert.InRange(item.Id, low: 1, high: 3));
        }

        [Fact]
        public void TestPlayerGetByNickname() {
            IPlayerRepository playerRepository = new PlayerMock();
            PlayerService playerService = new PlayerService(playerRepository, _mapper);

            PlayerBL expectedPlayer = new PlayerBL {
                Id = 1,
                Nickname = "NN1",
                HoursPlayed = 1,
                LastPlayed = new DateTime(2022, 5, 5)
            };

            PlayerBL actualPlayer = playerService.GetPlayerByNickname("NN1");

            Assert.Equal(expectedPlayer.Id, actualPlayer.Id);
            Assert.Equal(expectedPlayer.Nickname, actualPlayer.Nickname);
            Assert.Equal(expectedPlayer.HoursPlayed, actualPlayer.HoursPlayed);
            Assert.Equal(expectedPlayer.LastPlayed, actualPlayer.LastPlayed);
        }
    }
}