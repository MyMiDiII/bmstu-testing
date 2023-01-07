using ServerING.Models;
using ServerING.Repository;
using ServerING.Interfaces;
using ServerING.Services;
using ServerING.Utils;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Builder;

namespace Integrate {
    public class PlatformTest {
        private AppDBContent context;
        private IMapper mapper;

        public PlatformTest() {
            var builder = WebApplication.CreateBuilder();
            var config = builder
                .Configuration
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("dbsettings.json")
                .Build();

            var options = new DbContextOptionsBuilder<AppDBContent>()
                .UseNpgsql(config.GetConnectionString("DefaultConnection") ?? "Error")
                .Options;

            context = new AppDBContent(options);
            context.Database.EnsureCreated();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMappingProfile());
            });
            mapper = mockMapper.CreateMapper();
        }


        [Fact]
        public void GetById() {
            IPlatformRepository platformRepository = new PlatformRepository(context);
            IPlatformService platformService = new PlatformService(platformRepository, mapper);

            var result = platformService.GetPlatformByID(6);
            Assert.NotNull(result);
        }
    }
}
