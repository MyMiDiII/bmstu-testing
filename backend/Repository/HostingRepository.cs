using ServerING.Interfaces;
using ServerING.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServerING.Repository {
    public class HostingRepository : IHostingRepository {

        ///
        private readonly AppDBContent appDBContent;

        public HostingRepository(AppDBContent appDBContent) {
            this.appDBContent = appDBContent;
        }
        ///

        public WebHosting Add(WebHosting webHosting) {
            try {
                appDBContent.WebHosting.Add(webHosting);
                appDBContent.SaveChanges();

                return GetByID(webHosting.Id);
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("WebHosting Add Error");
            }
        }

        public WebHosting Delete(int id) {
            try {
                WebHosting webHosting = appDBContent.WebHosting.Find(id);

                if (webHosting == null) {
                    return null;
                }
                else {
                    appDBContent.WebHosting.Remove(webHosting);
                    appDBContent.SaveChanges();

                    return webHosting;
                }
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("WebHosting Delete Error");
            }
        }


        public WebHosting Update(WebHosting webHosting) {
            try {
                var curWebHosting = appDBContent.WebHosting.FirstOrDefault(x => x.Id == webHosting.Id);
                appDBContent.Entry(curWebHosting).CurrentValues.SetValues(webHosting);
                /*appDBContent.WebHosting.Update(webHosting);*/
                appDBContent.SaveChanges();

                return GetByID(webHosting.Id);
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("WebHosting Update Error");
            }
        }

        public IEnumerable<WebHosting> GetAll() {
            return appDBContent.WebHosting.ToList();
        }

        public WebHosting GetByID(int id) {
            return appDBContent.WebHosting.Find(id);
        }

        public WebHosting GetByName(string name) {
            return appDBContent.WebHosting.FirstOrDefault(w => w.Name == name);
        }

        public IEnumerable<WebHosting> GetByPricePerMonth(int pricePerMonth) {
            return appDBContent.WebHosting.Where(w => w.PricePerMonth == pricePerMonth).ToList();
        }

        public IEnumerable<WebHosting> GetBySubMonths(ushort subMonths) {
            return appDBContent.WebHosting.Where(w => w.SubMonths == subMonths).ToList();
        }
    }
}
