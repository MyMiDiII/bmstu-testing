using ServerING.DTO;
using ServerING.ModelsBL;
using ServerING.Services;

namespace ServerING.ModelConverters {
    public class PlayerConverters {

        private readonly IPlayerService playerService;

        public PlayerConverters(IPlayerService playerService) {
            this.playerService = playerService;
        }

        public PlayerBL convertPatch(int id, PlayerBaseDto player) {
            var existedPlayer = playerService.GetPlayerByID(id);

            return new PlayerBL {
                Id = id,
                Nickname = player.Nickname ?? existedPlayer.Nickname,
                HoursPlayed = player.HoursPlayed ?? existedPlayer.HoursPlayed,
                LastPlayed = player.LastPlayed ?? existedPlayer.LastPlayed
            };
        }
    }
}