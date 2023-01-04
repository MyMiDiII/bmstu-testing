using ServerING.Interfaces;
using ServerING.Models;
using System.Collections.Generic;
using System.Linq;


namespace ServerING.Mocks {
    /*
    public class WebHostingMock : IWebHostingRepository {

        private List<WebHosting> _webHostings = new List<WebHosting> {
            new WebHosting {
                Id = 1,
                Name = "WH1",
                PricePerMonth = 1000,
                SubMonths = 1
            },
            new WebHosting {
                Id = 2,
                Name = "WH2",
                PricePerMonth = 2000,
                SubMonths = 2
            },
            new WebHosting {
                Id = 3,
                Name = "WH3",
                PricePerMonth = 3000,
                SubMonths = 3
            }
        };


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
