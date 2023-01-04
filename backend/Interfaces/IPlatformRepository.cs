using ServerING.Models;

namespace ServerING.Interfaces {
    public interface IPlatformRepository : IRepository<Platform> {
        Platform GetByName(string name);
    }
}
