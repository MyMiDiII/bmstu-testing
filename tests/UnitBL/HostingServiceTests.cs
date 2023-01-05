using AutoMapper;

using ServerING.Utils;
using ServerING.Services;
using ServerING.ModelsBL;
using ServerING.Interfaces;

namespace UnitBL
{
    public class HostingServiceTests 
    {
        private IMapper _mapper;

        public HostingServiceTests() {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMappingProfile());
            });
            _mapper = mockMapper.CreateMapper();
        }

        [Fact]
        public void TestHostingAdd() {
            IHostingRepository hostingRepository = new HostingMock();
            HostingService hostingService = new HostingService(hostingRepository, _mapper);

            WebHostingBL expectedHosting = new WebHostingBL {
                Id = 4,
                Name = "WH4",
                PricePerMonth = 4000,
                SubMonths = 4
            };

            WebHostingBL actualHosting = hostingService.AddHosting(expectedHosting);

            Assert.NotEqual(0, actualHosting.Id);
            Assert.Equal(expectedHosting.Name, actualHosting.Name);
            Assert.Equal(expectedHosting.PricePerMonth, actualHosting.PricePerMonth);
            Assert.Equal(expectedHosting.SubMonths, actualHosting.SubMonths);
        }

        [Fact]
        public void TestHostingDelete() {
            IHostingRepository hostingRepository = new HostingMock();
            HostingService hostingService = new HostingService(hostingRepository, _mapper);

            WebHostingBL expectedHosting = new WebHostingBL {
                Id = 3,
                Name = "WH3",
                PricePerMonth = 3000,
                SubMonths = 3
            };

            WebHostingBL actualHosting = hostingService.DeleteHosting(3);

            Assert.Equal(expectedHosting.Id, actualHosting.Id);
            Assert.Equal(expectedHosting.Name, actualHosting.Name);
            Assert.Equal(expectedHosting.PricePerMonth, actualHosting.PricePerMonth);
            Assert.Equal(expectedHosting.SubMonths, actualHosting.SubMonths);
        }

        [Fact]
        public void TestHostingUpdate() {
            IHostingRepository hostingRepository = new HostingMock();
            HostingService hostingService = new HostingService(hostingRepository, _mapper);

            WebHostingBL expectedHosting = new WebHostingBL {
                Id = 1,
                Name = "WH1Changed",
                PricePerMonth = 1000,
                SubMonths = 1
            };

            WebHostingBL actualHosting = hostingService.UpdateHosting(1, expectedHosting);

            Assert.Equal(expectedHosting.Name, actualHosting.Name);
            Assert.Equal(expectedHosting.PricePerMonth, actualHosting.PricePerMonth);
            Assert.Equal(expectedHosting.SubMonths, actualHosting.SubMonths);
        }

        [Fact]
        public void TestHostingGetById() {
            IHostingRepository hostingRepository = new HostingMock();
            HostingService hostingService = new HostingService(hostingRepository, _mapper);

            WebHostingBL expectedHosting = new WebHostingBL {
                Id = 1,
                Name = "WH1",
                PricePerMonth = 1000,
                SubMonths = 1
            };

            WebHostingBL actualHosting = hostingService.GetHostingByID(1);

            Assert.Equal(expectedHosting.Id, actualHosting.Id);
            Assert.Equal(expectedHosting.Name, actualHosting.Name);
            Assert.Equal(expectedHosting.PricePerMonth, actualHosting.PricePerMonth);
            Assert.Equal(expectedHosting.SubMonths, actualHosting.SubMonths);
        }

        [Fact]
        public void TestHostingGetAll() {
            IHostingRepository hostingRepository = new HostingMock();
            HostingService hostingService = new HostingService(hostingRepository, _mapper);

            var hostings = hostingService.GetAllHostings();

            Assert.IsType<List<WebHostingBL>>(hostings);
            Assert.Equal(3, hostings.Count());
            Assert.All(hostings, item => Assert.InRange(item.Id, low: 1, high: 3));
        }

        [Fact]
        public void TestHostingGetByName() {
            IHostingRepository hostingRepository = new HostingMock();
            HostingService hostingService = new HostingService(hostingRepository, _mapper);

            WebHostingBL expectedHosting = new WebHostingBL {
                Id = 1,
                Name = "WH1",
                PricePerMonth = 1000,
                SubMonths = 1
            };

            WebHostingBL actualHosting = hostingService.GetHostingByName("WH1");

            Assert.Equal(expectedHosting.Id, actualHosting.Id);
            Assert.Equal(expectedHosting.Name, actualHosting.Name);
            Assert.Equal(expectedHosting.PricePerMonth, actualHosting.PricePerMonth);
            Assert.Equal(expectedHosting.SubMonths, actualHosting.SubMonths);
        }
    }
}