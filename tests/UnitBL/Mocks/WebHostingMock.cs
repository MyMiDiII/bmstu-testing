using ServerING.Interfaces;
using ServerING.Models;
using System.Collections.Generic;
using System.Linq;


namespace UnitBL {
    /*
    public class WebHostingMock : MockData, IWebHostingRepository {

        public void Add(WebHosting model) {
            _webHostings.Add(model);
        }

        public WebHosting Delete(int id) {
            WebHosting model = _webHostings[id - 1];
            _webHostings.Remove(model);

            return model;
        }

        public IEnumerable<WebHosting> GetAll() {
            return _webHostings;
        }

        public WebHosting GetByID(int id) {
            return _webHostings[id - 1];
        }

        public WebHosting GetByName(string name) {
            return _webHostings.FirstOrDefault(x => x.Name == name);
        }

        public IEnumerable<WebHosting> GetByPricePerMonth(int pricePerMonth) {
            return _webHostings.Where(x => x.PricePerMonth == pricePerMonth);
        }

        public IEnumerable<WebHosting> GetBySubMonths(ushort subMonths) {
            return _webHostings.Where(x =>x.SubMonths == subMonths);
        }

        public void Update(WebHosting model) {
            WebHosting webHosting = _webHostings[model.Id - 1];

            webHosting.Name = model.Name;
            webHosting.PricePerMonth = model.PricePerMonth;
            webHosting.SubMonths = model.SubMonths;

            _webHostings[model.Id - 1] = webHosting;
        }
    }
    */
}
