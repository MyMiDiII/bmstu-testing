using ServerING.Models;

namespace ServerING.Interfaces {
    public interface IHostingRepository : IRepository<WebHosting>{
        WebHosting GetByName(string name);
    }
}
