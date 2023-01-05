using ServerING.Interfaces;
using ServerING.Models;
using System.Collections.Generic;
using System.Linq;


namespace UnitBL {
    public class HostingMock : MockData, IHostingRepository {
        private List<WebHosting> _hostings = new List<WebHosting> {
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
        private int _nextID = 4;

        public WebHosting Add(WebHosting model) {
            model.Id  = _nextID;

            _nextID++;
            _hostings.Add(model);

            return _hostings.Last();
        }

        public WebHosting Delete(int id) {
            WebHosting hosting = _hostings.First(x => x.Id == id);
            _hostings.Remove(hosting);

            return hosting;
        }

        public IEnumerable<WebHosting> GetAll() {
            return _hostings;
        }

        public WebHosting GetByID(int id) {
            return _hostings.First(x => x.Id == id);
        }

        public WebHosting GetByName(string name) {
            return _hostings.FirstOrDefault(x => x.Name == name) ?? new WebHosting();
        }

        public WebHosting Update(WebHosting model) {
            WebHosting hosting= _hostings[model.Id - 1];

            hosting.Id = model.Id;
            hosting.Name = model.Name;
            hosting.PricePerMonth = model.PricePerMonth;
            hosting.SubMonths = model.SubMonths;

            _hostings[model.Id - 1] = hosting;

            return hosting;
        }
    }
}
