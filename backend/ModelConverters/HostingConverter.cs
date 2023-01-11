using ServerING.DTO;
using ServerING.ModelsBL;
using ServerING.Services;

namespace ServerING.ModelConverters {
    public class HostingConverters {

        private readonly IHostingService hostingService;

        public HostingConverters(IHostingService hostingService) {
            this.hostingService = hostingService;
        }

        public WebHostingBL convertPatch(int id, HostingBaseDto platform) {
            var existedHosting = hostingService.GetHostingByID(id);

            return new WebHostingBL {
                Id = id,
                Name = platform.Name ?? existedHosting.Name,
                PricePerMonth = platform.PricePerMonth ?? existedHosting.PricePerMonth,
                SubMonths = platform.SubMonths ?? existedHosting.SubMonths
            };
        }
    }
}