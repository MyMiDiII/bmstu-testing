using ServerING.Services;
using ServerING.ModelsBL;
using Integrate.Utils;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Allure.Xunit.Attributes;
using AutoFixture;


namespace Integrate {
    [AllureOwner("EqualNine")]
    [AllureSuite("Player Integration Tests")]
    public class PlayerIntegTest {
        private PlayerService playerService;

        public PlayerIntegTest() {
            var server = new TestServer(new WebHostBuilder().UseStartup<TestStartup>());
            playerService = (PlayerService)server.Services.GetRequiredService<IPlayerService>();
        }

        [AllureXunit]
        [ResetDatabase]
        public void TestPlayerAdd() {
            var fixture = new Fixture();
            PlayerBL player = fixture.Create<PlayerBL>();
            player.LastPlayed = (new DateTime(2023, 1, 1)).ToUniversalTime();

            PlayerBL result = playerService.AddPlayer(player);

            Assert.Equal(player.Nickname, result.Nickname);
            Assert.Equal(player.LastPlayed, result.LastPlayed);
            Assert.Equal(player.HoursPlayed, result.HoursPlayed);
        }

        [AllureXunitTheory]
        [InlineData(1)]
        [ResetDatabase]
        public void TestPlayerUpdate(int id) {
            var fixture = new Fixture();
            PlayerBL player = fixture.Create<PlayerBL>();
            player.Id = id;
            player.LastPlayed = (new DateTime(2023, 1, 1)).ToUniversalTime();

            PlayerBL result = playerService.UpdatePlayer(id, player);

            Assert.Equal(player.Nickname, result.Nickname);
            Assert.Equal(player.LastPlayed, result.LastPlayed);
            Assert.Equal(player.HoursPlayed, result.HoursPlayed);
        }

        [AllureXunitTheory]
        [InlineData(1)]
        [ResetDatabase]
        public void TestPlayerDelete(int id) {
            PlayerBL result = playerService.DeletePlayer(id);

            Assert.NotNull(result);
        }

        [AllureXunitTheory]
        [InlineData(1)]
        [ResetDatabase]
        public void TestPlayerGetById(int id) {
            var result = playerService.GetPlayerByID(id);

            Assert.NotNull(result);
        }

        [AllureXunit]
        [ResetDatabase]
        public void TestPlayerGetAll() {
            var result = playerService.GetAllPlayers();

            Assert.NotNull(result);
        }

        [AllureXunitTheory]
        [InlineData("Player1")]
        [ResetDatabase]
        public void TestPlayerGetByNickname(String nickname) {
            var result = playerService.GetPlayerByNickname(nickname);

            Assert.NotNull(result);
        }
    }
}
