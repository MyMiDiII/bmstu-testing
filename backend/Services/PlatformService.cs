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
    public interface IPlatformService {
        PlatformBL AddPlatform(PlatformBL platform);
        PlatformBL DeletePlatform(int id);
        PlatformBL UpdatePlatform(int id, PlatformBL platform);

        PlatformBL GetPlatformByID(int id);
        IEnumerable<PlatformBL> GetAllPlatforms();

        PlatformBL GetPlatformByName(string name);
        IEnumerable<PlatformBL> GetPlatformByPopularity(ushort popularity);
        IEnumerable<PlatformBL> GetPlatformByCost(int cost);
    }

    public class PlatformService : IPlatformService {

        private readonly IPlatformRepository platformRepository;
        private IMapper mapper;

        public PlatformService(IPlatformRepository platformRepository, IMapper mapper) {
            this.platformRepository = platformRepository;
            this.mapper = mapper;
        }


        private bool IsExist(PlatformBL platform) {
            return platformRepository
                .GetAll()
                .Where(item => item.Id != platform.Id)
                .Any(item => item.Name == platform.Name);
        }


        private bool IsExistById(int id) {
            return platformRepository.GetByID(id) != null;
        }


        public PlatformBL AddPlatform(PlatformBL platform) {
            if (IsExist(platform)) {
                var conflictedId = platformRepository.GetByName(platform.Name).Id;
                throw new PlatformConflictException(conflictedId);
            }

            var transferedPlatform = mapper.Map<Platform>(platform);
            return mapper.Map<PlatformBL>(platformRepository.Add(transferedPlatform));
        }

        public PlatformBL DeletePlatform(int id) {
            return mapper.Map<PlatformBL>(platformRepository.Delete(id));
        }

        public PlatformBL GetPlatformByID(int id) {
            return mapper.Map<PlatformBL>(platformRepository.GetByID(id));
        }

        public IEnumerable<PlatformBL> GetAllPlatforms() {
            return mapper.Map<IEnumerable<PlatformBL>>(platformRepository.GetAll());
        }

        public PlatformBL UpdatePlatform(int id, PlatformBL platform) {
            if (IsExist(platform)) {
                var conflictedId = platformRepository.GetByName(platform.Name).Id;
                throw new PlatformConflictException(conflictedId);
            }

            if (!IsExistById(id))
                return null;

            var transferedPlatform = mapper.Map<Platform>(platform);
            return mapper.Map<PlatformBL>(platformRepository.Update(transferedPlatform));
        }

        public PlatformBL GetPlatformByName(string name) {
            return mapper.Map<PlatformBL>(platformRepository.GetByName(name));
        }

        public IEnumerable<PlatformBL> GetPlatformByPopularity(ushort popularity) {
            return mapper.Map<IEnumerable<PlatformBL>>(platformRepository.GetByPopularity(popularity));
        }

        public IEnumerable<PlatformBL> GetPlatformByCost(int cost) {
            return mapper.Map<IEnumerable<PlatformBL>>(platformRepository.GetByCost(cost));
        }
    }
}
