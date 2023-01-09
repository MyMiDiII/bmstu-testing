using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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

            _configuration["TestConnection"] = "Server=localhost;Port=5432;Database=testdb; User Id=postgres;Password=postgres";
        }

        public override void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<AppDBContent>(
                options => options.UseNpgsql(_configuration["TestConnection"] ?? "Error"),
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
            // services.AddControllers();
            services.AddControllers().AddApplicationPart(typeof(Startup).Assembly);

            // AutoMapper
            services.AddAutoMapper(typeof(AutoMappingProfile));

            // DTO converters
            services.AddDtoConverters();

            // CORS
            services.AddCors(options =>{
                options.AddPolicy(name: "MyPolicy",
                    policy => {
                        policy
                            .WithOrigins("*")
                            .WithHeaders("*")
                            .WithMethods("*");
                    });
            });
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory) {
            app.UseRouting();
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
