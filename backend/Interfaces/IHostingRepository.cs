using ServerING.Models;
using System.Collections.Generic;

namespace ServerING.Interfaces {
    public interface IHostingRepository : IRepository<WebHosting>{

        WebHosting GetByName(string name);
        IEnumerable<WebHosting> GetByPricePerMonth(int pricePerMonth);
        IEnumerable<WebHosting> GetBySubMonths(ushort subMonths);
    }
}
