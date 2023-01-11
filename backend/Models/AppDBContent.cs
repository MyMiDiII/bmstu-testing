using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace ServerING.Models {
    public class AppDBContent : DbContext {

        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Platform> Platform { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<WebHosting> WebHosting { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<Server> Server { get; set; }

        public DbSet<ServerPlayer> ServerPlayer { get; set; }
        public DbSet<FavoriteServer> FavoriteServer { get; set; }
    }
}