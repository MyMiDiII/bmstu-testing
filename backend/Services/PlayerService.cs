using ServerING.Interfaces;
using ServerING.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using ServerING.Exceptions;
using ServerING.ModelsBL;
using AutoMapper;

namespace ServerING.Services {

    public interface IPlayerService {
        PlayerBL AddPlayer(PlayerBL player);
        PlayerBL UpdatePlayer(int id, PlayerBL player);
        PlayerBL DeletePlayer(int id);

        PlayerBL GetPlayerByID(int id);
        IEnumerable<PlayerBL> GetAllPlayers();

        PlayerBL GetPlayerByNickname(string nickname);
        IEnumerable<PlayerBL> GetPlayersByHoursPlayed(int hoursPlayed);
        IEnumerable<PlayerBL> GetPlayersByLastPlayed(DateTime lastPlayed);
    }


    public class PlayerService : IPlayerService {
        private readonly IPlayerRepository playerRepository;
        private readonly IMapper mapper;

        public PlayerService(IPlayerRepository playerRepository, IMapper mapper) {
            this.playerRepository = playerRepository;
            this.mapper = mapper;
        }


        private bool IsExist(PlayerBL player) {
            return playerRepository.GetAll()
                .Where(item => item.Id != player.Id)
                .Any(item => item.Nickname == player.Nickname);
        }


        private bool IsExistById(int id) {
            return playerRepository.GetByID(id) != null;
        }

        public PlayerBL AddPlayer(PlayerBL player) {
            if (IsExist(player))
                throw new PlayerAlreadyExistsException("Player already exists");

            var transferedPlayer = mapper.Map<Player>(player);
            return mapper.Map<PlayerBL>(playerRepository.Add(transferedPlayer));
        }

        public PlayerBL DeletePlayer(int id) {
            return mapper.Map<PlayerBL>(playerRepository.Delete(id));
        }

        public IEnumerable<PlayerBL> GetAllPlayers() {
            return mapper.Map<IEnumerable<PlayerBL>>(playerRepository.GetAll());
        }

        public PlayerBL GetPlayerByID(int id) {
            return mapper.Map<PlayerBL>(playerRepository.GetByID(id));
        }

        public PlayerBL GetPlayerByNickname(string nickname) {
            return mapper.Map<PlayerBL>(playerRepository.GetByNickname(nickname));
        }

        public IEnumerable<PlayerBL> GetPlayersByHoursPlayed(int hoursPlayed) {
            return mapper.Map<IEnumerable<PlayerBL>>(playerRepository.GetByHoursPlayed(hoursPlayed));
        }

        public IEnumerable<PlayerBL> GetPlayersByLastPlayed(DateTime lastPlayed) {
            return mapper.Map<IEnumerable<PlayerBL>>(playerRepository.GetByLastPlayed(lastPlayed));
        }

        public PlayerBL UpdatePlayer(int id, PlayerBL player) {
            if (!IsExistById(id))
                return null;

            if (IsExist(player))
                throw new PlayerAlreadyExistsException("Player already exists");

            var transferedPlayer = mapper.Map<Player>(player);
            return mapper.Map<PlayerBL>(playerRepository.Update(transferedPlayer));
        }
    }
}
