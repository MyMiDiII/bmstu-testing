using ServerING.DTO;
using ServerING.ModelsBL;
using ServerING.Services;

namespace ServerING.ModelConverters {
    public class ServerConverters {

        private readonly IServerService serverService;

        public ServerConverters(IServerService serverService) {
            this.serverService = serverService;
        }

        public ServerBL convertPatch(int id, ServerUpdateDto server) {
            var existedServer = serverService.GetServerByID(id);

            return new ServerBL {
                Id = id,
                Name = server.Name ?? existedServer.Name,
                GameName = server.GameName ?? existedServer.GameName,
                Ip = server.Ip ?? existedServer.Ip,
                Status = server.Status ?? existedServer.Status,
                Rating = server.Rating ?? existedServer.Rating,
                HostingID = server.HostingID ?? existedServer.HostingID,
                PlatformID = server.PlatformID ?? existedServer.PlatformID,
                CountryID = server.CountryID ?? existedServer.CountryID,
                OwnerID = server.OwnerID ?? existedServer.OwnerID
            };
        }
    }
}