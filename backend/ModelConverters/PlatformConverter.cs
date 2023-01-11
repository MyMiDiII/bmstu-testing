using ServerING.DTO;
using ServerING.ModelsBL;
using ServerING.Services;

namespace ServerING.ModelConverters {
    public class PlatformConverters {

        private readonly IPlatformService platformService;

        public PlatformConverters(IPlatformService platformService) {
            this.platformService = platformService;
        }

        public PlatformBL convertPatch(int id, PlatformBaseDto platform) {
            var existedPlatform = platformService.GetPlatformByID(id);

            return new PlatformBL {
                Id = id,
                Name = platform.Name ?? existedPlatform.Name,
                Cost = platform.Cost ?? existedPlatform.Cost,
                Popularity = platform.Popularity ?? existedPlatform.Popularity
            };
        }
    }
}