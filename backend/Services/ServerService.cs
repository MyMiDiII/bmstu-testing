using AutoMapper;
using ServerING.ModelsBL;
using ServerING.Interfaces;
using ServerING.Models;
using ServerING.ViewModels;
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
        IEnumerable<ServerBL> GetAllServers(
            ServerFilterDto filter,
            ServerSortState? sortState,
            int? page,
            int? pageSize
        );

        public IEnumerable<PlayerBL> GetServerPlayers(int serverId);

        Server GetServerByName(string name);
        ServerBL GetServerByIP(string ip);

        IEnumerable<Server> GetServersByGameName(string gameVersion);
        IEnumerable<Server> GetServersByHostingID(int id);
        IEnumerable<Server> GetServersByPlatformID(int id);
        IEnumerable<Server> GetServersByRating(int rating);

        IEnumerable<ServerBL> FilterServers(IEnumerable<ServerBL> servers, ServerFilterDto filter);
        IEnumerable<ServerBL> SortServersByOption(IEnumerable<ServerBL> servers, ServerSortState sortOrder);
        IEnumerable<ServerBL> PaginationServers(IEnumerable<ServerBL> servers, int page, int pageSize);
        IEnumerable<int> GetUserFavoriteServersIds(int userId);

        void UpdateServerRating(int serverId, int change);

        bool IsServerExists(Server server);
    }

    public class ServerService : IServerService {

        private readonly IServerRepository serverRepository;
        private readonly IPlatformRepository platformRepository;
        private readonly IUserRepository userRepository;
        private readonly IHostingRepository hostingRepository;
        private readonly IMapper mapper;

        public ServerService(IServerRepository serverRepository, 
                IPlatformRepository platformRepository, 
                IUserRepository userRepository,
                IHostingRepository hostingRepository,
                IMapper mapper) {
            this.serverRepository = serverRepository;
            this.platformRepository = platformRepository;
            this.userRepository = userRepository;
            this.hostingRepository = hostingRepository;
            this.mapper = mapper;
        }

        public IEnumerable<ServerBL> GetAllServers(
            ServerFilterDto filter, 
            ServerSortState? sortState,
            int? page,
            int? pageSize
        ) {
            var servers = mapper.Map<IEnumerable<ServerBL>>(serverRepository.GetAll());

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

        public Server GetServerByName(string name) {
            return serverRepository.GetByName(name);
        }

        public IEnumerable<Server> GetServersByGameName(string gameName) {
            return serverRepository.GetByGameName(gameName);
        }

        public IEnumerable<Server> GetServersByHostingID(int id) {
            return serverRepository.GetByWebHostingID(id);
        }

        public IEnumerable<Server> GetServersByPlatformID(int id) {
            return serverRepository.GetByPlatformID(id);
        }

        public ServerBL GetServerByIP(string ip) {
            return mapper.Map<ServerBL>(serverRepository.GetByIP(ip));
        }


        public IEnumerable<PlayerBL> GetServerPlayers(int serverId) {
            if (!IsExistById(serverId))
                throw new ServerNotExistsException("No server with such id");

            return mapper.Map<IEnumerable<PlayerBL>>(serverRepository.GetPlayersByServerID(serverId));
        }


        public IEnumerable<ServerBL> FilterServers(IEnumerable<ServerBL> servers, ServerFilterDto filter) {
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

        public IEnumerable<ServerBL> SortServersByOption(IEnumerable<ServerBL> servers, ServerSortState sortOrder) {

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

        public IEnumerable<ServerBL> PaginationServers(IEnumerable<ServerBL> servers, int page, int pageSize) {
            var paginatedServers = servers.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return paginatedServers;
        }

        public IEnumerable<Server> GetServersByRating(int rating) {
            return serverRepository.GetByRating(rating);
        }

        public void UpdateServerRating(int serverId, int change) {
            Server server = serverRepository.GetByID(serverId);
            server.Rating += change;

            serverRepository.Update(server);
        }

        public IEnumerable<int> GetUserFavoriteServersIds(int userId) {
            return userRepository.GetFavoriteServersByUserId(userId).Select(s => s.Id);
        }


        public bool IsServerExists(Server server) {
            
            Server serverCheckName = serverRepository.GetByName(server.Name);

            if (serverCheckName != null && server.Id != serverCheckName.Id) {
                return true;
            }

            Server serverCheckIP = serverRepository.GetByIP(server.Ip);

            if (serverCheckIP != null && server.Id != serverCheckIP.Id) {
                return true;
            }

            return false;
        }
    }
}
