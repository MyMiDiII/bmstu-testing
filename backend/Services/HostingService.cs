using AutoMapper;
using ServerING.DTO;
using ServerING.Exceptions;
using ServerING.Interfaces;
using ServerING.Models;
using ServerING.ModelsBL;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ServerING.Services {
    public interface IHostingService {
        WebHostingBL AddHosting(WebHostingBL hosting);
        WebHostingBL UpdateHosting(int id, WebHostingBL hosting);
        WebHostingBL DeleteHosting(int id);

        WebHostingBL GetHostingByID(int id);
        IEnumerable<WebHostingBL> GetAllHostings();

        WebHostingBL GetHostingByName(string name);
        IEnumerable<WebHostingBL> GetHostingByPricePerMonth(int pricePerMonth);
        IEnumerable<WebHostingBL> GetHostingBySubMonths(ushort subMonths);
    }


    public class HostingService : IHostingService {
        private readonly IHostingRepository hostingRepository;
        private readonly IMapper mapper;

        public HostingService(IHostingRepository hostingRepository, IMapper mapper) {
            this.hostingRepository = hostingRepository;
            this.mapper = mapper;
        }

        private bool IsExist(WebHostingBL hosting) {
            return hostingRepository.GetAll()
                .Where(item => item.Id != hosting.Id)
                .Any(item => item.Name == hosting.Name);
        }

        private bool IsExistById(int id) {
            return hostingRepository.GetByID(id) != null;
        }

        public WebHostingBL AddHosting(WebHostingBL hosting) {
            if (IsExist(hosting)) {
                var conflictedId = hostingRepository.GetByName(hosting.Name).Id;
                throw new HostingConflictException(conflictedId);
            }

            var transferedHosting = mapper.Map<WebHosting>(hosting);
            return mapper.Map<WebHostingBL>(hostingRepository.Add(transferedHosting));
        }

        public WebHostingBL UpdateHosting(int id, WebHostingBL hosting) {
            if (IsExist(hosting)) {
                var conflictedId = hostingRepository.GetByName(hosting.Name).Id;
                throw new HostingConflictException(conflictedId);
            }

            if (!IsExistById(id))
                return null;
            
            var transferedHosting = mapper.Map<WebHosting>(hosting);
            return mapper.Map<WebHostingBL>(hostingRepository.Update(transferedHosting));
        }

        public WebHostingBL DeleteHosting(int id) {
            return mapper.Map<WebHostingBL>(hostingRepository.Delete(id));
        }

        public IEnumerable<WebHostingBL> GetAllHostings() {
            return mapper.Map<IEnumerable<WebHostingBL>>(hostingRepository.GetAll());
        }

        public WebHostingBL GetHostingByID(int id) {
            return mapper.Map<WebHostingBL>(hostingRepository.GetByID(id));
        }

        public IEnumerable<WebHostingBL> GetHostingByPricePerMonth(int pricePerMonth) {
            return mapper
                .Map<IEnumerable<WebHostingBL>>(hostingRepository.GetByPricePerMonth(pricePerMonth));
        }

        public IEnumerable<WebHostingBL> GetHostingBySubMonths(ushort subMonths) {
            return mapper
                .Map<IEnumerable<WebHostingBL>>(hostingRepository.GetBySubMonths(subMonths));
        }

        public WebHostingBL GetHostingByName(string name) {
            return mapper.Map<WebHostingBL>(hostingRepository.GetByName(name));
        }
    }
}
