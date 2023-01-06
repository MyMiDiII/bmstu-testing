using AutoMapper;

using Allure.Xunit.Attributes;

using ServerING.Utils;
using ServerING.Services;
using ServerING.ModelsBL;
using ServerING.Interfaces;

namespace UnitBL
{
    [AllureOwner("EqualNine")]
    [AllureSuite("Platform Service Tests")]
    public class PlatformServiceTests 
    {
        private IMapper _mapper;

        public PlatformServiceTests() {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMappingProfile());
            });
            _mapper = mockMapper.CreateMapper();
        }

        [AllureXunit]
        public void TestPlatformAdd() {
            IPlatformRepository platformRepository = new PlatformStub();
            PlatformService platformService = new PlatformService(platformRepository, _mapper);

            PlatformBL expectedPlatform = new PlatformBL {
                Name = "Platform4",
                Popularity = 4,
                Cost = 4000
            };

            PlatformBL actualPlatform = platformService.AddPlatform(expectedPlatform);

            Assert.NotEqual(0, actualPlatform.Id);
            Assert.Equal(expectedPlatform.Name, actualPlatform.Name);
            Assert.Equal(expectedPlatform.Popularity, actualPlatform.Popularity);
            Assert.Equal(expectedPlatform.Cost, actualPlatform.Cost);
        }

        [AllureXunit]
        public void TestPlatformDelete() {
            IPlatformRepository platformRepository = new PlatformStub();
            PlatformService platformService = new PlatformService(platformRepository, _mapper);

            PlatformBL expectedPlatform = new PlatformBL {
                Id = 3,
                Name = "Platform3",
                Popularity = 3,
                Cost = 3000
            };

            PlatformBL actualPlatform = platformService.DeletePlatform(3);

            Assert.Equal(expectedPlatform.Id, actualPlatform.Id);
            Assert.Equal(expectedPlatform.Name, actualPlatform.Name);
            Assert.Equal(expectedPlatform.Popularity, actualPlatform.Popularity);
            Assert.Equal(expectedPlatform.Cost, actualPlatform.Cost);
        }

        [AllureXunit]
        public void TestPlatformUpdate() {
            IPlatformRepository platformRepository = new PlatformStub();
            PlatformService platformService = new PlatformService(platformRepository, _mapper);

            PlatformBL expectedPlatform = new PlatformBL {
                Id = 1,
                Name = "Platform1Changed",
                Popularity = 1,
                Cost = 1000
            };

            PlatformBL actualPlatform = platformService.UpdatePlatform(1, expectedPlatform);

            Assert.Equal(expectedPlatform.Name, actualPlatform.Name);
            Assert.Equal(expectedPlatform.Popularity, actualPlatform.Popularity);
            Assert.Equal(expectedPlatform.Cost, actualPlatform.Cost);
        }

        [AllureXunit]
        public void TestPlatformGetById() {
            IPlatformRepository platformRepository = new PlatformStub();
            PlatformService platformService = new PlatformService(platformRepository, _mapper);

            PlatformBL expectedPlatform = new PlatformBL {
                Id = 1,
                Name = "Platform1",
                Popularity = 1,
                Cost = 1000
            };

            PlatformBL actualPlatform = platformService.GetPlatformByID(1);

            Assert.Equal(expectedPlatform.Id, actualPlatform.Id);
            Assert.Equal(expectedPlatform.Name, actualPlatform.Name);
            Assert.Equal(expectedPlatform.Popularity, actualPlatform.Popularity);
            Assert.Equal(expectedPlatform.Cost, actualPlatform.Cost);
        }

        [AllureXunit]
        public void TestPlatformGetAll() {
            IPlatformRepository platformRepository = new PlatformStub();
            PlatformService platformService = new PlatformService(platformRepository, _mapper);

            var platforms = platformService.GetAllPlatforms();

            Assert.IsType<List<PlatformBL>>(platforms);
            Assert.Equal(3, platforms.Count());
            Assert.All(platforms, item => Assert.InRange(item.Id, low: 1, high: 3));
        }

        [AllureXunit]
        public void TestPlatformGetByName() {
            IPlatformRepository platformRepository = new PlatformStub();
            PlatformService platformService = new PlatformService(platformRepository, _mapper);

            PlatformBL expectedPlatform = new PlatformBL {
                Id = 1,
                Name = "Platform1",
                Popularity = 1,
                Cost = 1000
            };

            PlatformBL actualPlatform = platformService.GetPlatformByName("Platform1");

            Assert.Equal(expectedPlatform.Id, actualPlatform.Id);
            Assert.Equal(expectedPlatform.Name, actualPlatform.Name);
            Assert.Equal(expectedPlatform.Popularity, actualPlatform.Popularity);
            Assert.Equal(expectedPlatform.Cost, actualPlatform.Cost);
        }
    }
}