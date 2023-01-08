using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ServerING;
using ServerING.Interfaces;
using ServerING.Models;
using ServerING.Repository;
using ServerING.Services;
using ServerING.Utils;

namespace Integrate {
    public class TestStartup: Startup {
        private IConfigurationRoot _configuration;
        
        public TestStartup(IWebHostEnvironment hostEnv): base(hostEnv) {
            // Тянет из файла dbsettings.json в папке backend почему-то... Не работает, использую строку подключения другую
            _configuration = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
            
            _configuration["DefaultConnection"] = "Server=localhost;Port=5432;Database=test_db; User Id=amunra23;Password=postgres";
        }

        public override void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<AppDBContent>(
                options => options.UseNpgsql(_configuration["DefaultConnection"] ?? "Error"),
                ServiceLifetime.Transient
            );

            // Services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IServerService, ServerService>();
            services.AddTransient<IPlatformService, PlatformService>();
            services.AddTransient<IHostingService, HostingService>();
            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<ICountryService, CountryService>();

            // Repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IServerRepository, ServerRepository>();
            services.AddTransient<IHostingRepository, HostingRepository>();
            services.AddTransient<IPlatformRepository, PlatformRepository>();
            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();

            // Controllers
            services.AddControllers();

            // AutoMapper
            services.AddAutoMapper(typeof(AutoMappingProfile));
        }
    }
}
