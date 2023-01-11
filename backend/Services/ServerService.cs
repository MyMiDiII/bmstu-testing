using AutoMapper;
using ServerING.ModelsBL;
using ServerING.Interfaces;
using ServerING.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ServerING.DTO;
using ServerING.Exceptions;
using ServerING.Enums;

namespace ServerING.Services {

    public interface IServerService {
        ServerBL AddServer(ServerBL server);
        ServerBL DeleteServer(int id);
        ServerBL UpdateServer(ServerBL server);

        ServerBL GetServerByID(int id);
        ServerBL GetServerByName(string name);
        IEnumerable<ServerBL> GetAllServers(
            ServerFilterDto filter,
            ServerSortState? sortState,
            int? page,
            int? pageSize
        );

        public IEnumerable<PlayerBL> GetServerPlayers(int serverId);

        IEnumerable<ServerBL> ProcessServers(
            IEnumerable<ServerBL> servers,
            ServerFilterDto filter,
            ServerSortState? sortOrder,
            int? page,
            int? pageSize
            );
        void UpdateServerRating(int serverId, int change);
    }

    public class ServerService : IServerService {

        private readonly IServerRepository serverRepository;
        private readonly IMapper mapper;

        public ServerService(IServerRepository serverRepository, 
                IMapper mapper) {
            this.serverRepository = serverRepository;
            this.mapper = mapper;
        }

        public IEnumerable<ServerBL> GetAllServers(
            ServerFilterDto filter, 
            ServerSortState? sortState,
            int? page,
            int? pageSize
        ) {
            var servers = mapper.Map<IEnumerable<ServerBL>>(serverRepository.GetAll());

            return ProcessServers(servers, filter, sortState, page, pageSize);
        }

        public IEnumerable<ServerBL> ProcessServers(
            IEnumerable<ServerBL> servers,
            ServerFilterDto filter,
            ServerSortState? sortState,
            int? page,
            int? pageSize
            )
        {
            // Фильтрация
            servers = FilterServers(servers, filter);

            // Сортировка
            if (sortState != null) {
                servers = SortServersByOption(servers, sortState.Value);
            }

            // Пагинация
            if (page != null) {
                servers = PaginationServers(servers, page.Value, pageSize.Value);
            }
            
            return servers;
        }

        public ServerBL AddServer(ServerBL server) {
            if (IsExist(server)) {
                var conflictedId = serverRepository.GetByIP(server.Ip).Id;
                throw new ServerConflictException(conflictedId);
            }

            return mapper.Map<ServerBL>(serverRepository.Add(mapper.Map<Server>(server)));
        }

        public ServerBL GetServerByID(int id) {
            return mapper.Map<ServerBL>(serverRepository.GetByID(id));
        }

        private bool IsExist(ServerBL server) {
            return serverRepository.GetAll()
                .Where(item => item.Id != server.Id)
                .Any(item => item.Ip == server.Ip);
        }

        private bool IsExistById(int id) {
            return serverRepository.GetByID(id) != null;
        }

        public ServerBL UpdateServer(ServerBL server) {
            if (IsExist(server)) {
                var conflictedId = serverRepository.GetByIP(server.Ip).Id;
                throw new ServerConflictException(conflictedId);
            }

            if (!IsExistById(server.Id))
                throw null;

            return mapper.Map<ServerBL>(serverRepository.Update(mapper.Map<Server>(server)));
        }

        public ServerBL DeleteServer(int id) {
            return mapper.Map<ServerBL>(serverRepository.Delete(id));
        }

        public ServerBL GetServerByName(string name) {
            return mapper.Map<ServerBL>(serverRepository.GetByName(name));
        }

        public IEnumerable<PlayerBL> GetServerPlayers(int serverId) {
            if (!IsExistById(serverId))
                throw new ServerNotExistsException("No server with such id");

            return mapper.Map<IEnumerable<PlayerBL>>(serverRepository.GetPlayersByServerID(serverId));
        }


        private IEnumerable<ServerBL> FilterServers(IEnumerable<ServerBL> servers, ServerFilterDto filter) {
            var filteredServers = servers;

            if (filter.OwnerID != null) {
                filteredServers = filteredServers.Where(s => s.OwnerID == filter.OwnerID);
            }

            if (filter.Status != null) {
                filteredServers = filteredServers.Where(s => s.Status == filter.Status.Value);
            }

            if (!String.IsNullOrEmpty(filter.Name)) {
                filteredServers = filteredServers.Where(s => s.Name.Contains(filter.Name));
            }

            if (!String.IsNullOrEmpty(filter.Game)) {
                filteredServers = filteredServers.Where(s => s.GameName.Contains(filter.Game));
            }

            if (filter.PlatformID != null && filter.PlatformID != 0) {
                filteredServers = filteredServers.Where(s => s.PlatformID == filter.PlatformID);
            }

            return filteredServers;
        }

        private IEnumerable<ServerBL> SortServersByOption(IEnumerable<ServerBL> servers, ServerSortState sortOrder) {
            IEnumerable<ServerBL> filteredServers;

            if (sortOrder == ServerSortState.NameDesc) {
                filteredServers = servers.OrderByDescending(s => s.Name);
            }
            else if (sortOrder == ServerSortState.IPAsc) {
                filteredServers = servers.OrderBy(s => s.Ip);
            }
            else if (sortOrder == ServerSortState.IPDesc) {
                filteredServers = servers.OrderByDescending(s => s.Ip);
            }
            else if (sortOrder == ServerSortState.GameNameAsc) {
                filteredServers = servers.OrderBy(s => s.GameName);
            }
            else if (sortOrder == ServerSortState.GameNameDesc) {
                filteredServers = servers.OrderByDescending(s => s.GameName);
            }
            else if (sortOrder == ServerSortState.RatingAsc) {
                filteredServers = servers.OrderBy(s => s.Rating);
            }
            else if (sortOrder == ServerSortState.RatingDesc) {
                filteredServers = servers.OrderByDescending(s => s.Rating);
            }
            else if (sortOrder == ServerSortState.PlatformAsc) {
                filteredServers = servers.OrderBy(s => s.Platform.Name);
            }
            else if (sortOrder == ServerSortState.PlatformDesc) {
                filteredServers = servers.OrderByDescending(s => s.Platform.Name);
            }
            else {
                filteredServers = servers.OrderBy(s => s.Name);
            }

            return filteredServers;
        }

        private IEnumerable<ServerBL> PaginationServers(IEnumerable<ServerBL> servers, int page, int pageSize) {
            var paginatedServers = servers.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return paginatedServers;
        }

        public void UpdateServerRating(int serverId, int change) {
            Server server = serverRepository.GetByID(serverId);
            server.Rating += change;

            serverRepository.Update(server);
        }
    }
}
