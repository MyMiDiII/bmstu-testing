using AutoMapper;

using ServerING.Utils;
using ServerING.Services;
using ServerING.ModelsBL;
using ServerING.Interfaces;

namespace UnitBL
{
    public class PlatformServiceTests 
    {
        [Fact]
        public void TestPlatformAdd() {
            IPlatformRepository platformRepository = new PlatformMock();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            PlatformService platformService = new PlatformService(platformRepository, mapper);

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

    }
}