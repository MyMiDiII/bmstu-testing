using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServerING.DTO;
using ServerING.Exceptions;
using ServerING.ModelsBL;
using ServerING.Models;
using ServerING.Services;
using System.Linq;
using ServerING.Enums;
using AutoMapper;
using ServerING.ModelConverters;
using Microsoft.AspNetCore.Cors;

namespace ServerING.Controllers {
    [EnableCors("MyPolicy")]
    [ApiController]
    [Route("/api/v1/servers")]
    public class ServerController : Controller {
        private IServerService serverService;
        private ServerConverters serverConverters;
        private IMapper mapper;

        public ServerController(IServerService serverService, IMapper mapper, ServerConverters serverConverters) {
            this.serverService = serverService;
            this.mapper = mapper;
            this.serverConverters = serverConverters;
        }

        [EnableCors("MyPolicy")]
        [HttpGet]
        public IActionResult GetAll(
            [FromQuery] ServerFilterDto filter,
            [FromQuery] ServerSortState? sortState,
            [FromQuery] int? page,
            [FromQuery] int? pageSize
        ) {
            return Ok(mapper.Map<IEnumerable<ServerDto>>(serverService.GetAllServers(filter, sortState, page, pageSize)));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Add(ServerDtoBase server) {
            try {
                var serverBL = mapper.Map<ServerBL>(server);
                var addedServer =  mapper.Map<ServerDto>(serverService.AddServer(serverBL));
                return Ok(addedServer);
            }
            catch (ServerConflictException ex) {
                return Conflict(ex.Message);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id) {
            var server = serverService.GetServerByID(id);
            return server != null ? Ok(mapper.Map<ServerDto>(server)) : NotFound();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ServerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Put(int id, ServerUpdateDto server) {
            try {
                var updatedServer = serverService.UpdateServer(mapper.Map<ServerBL>(server,
                o => o.AfterMap((src, dest) => dest.Id = id)));
                return updatedServer != null ? Ok(mapper.Map<ServerDto>(updatedServer)) : NotFound();
            }
            catch (ServerConflictException ex) {
                return Conflict(ex.Message);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(Server), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Patch(int id, ServerUpdateDto server) {
            try {
                var updatedServer = serverService.UpdateServer(serverConverters.convertPatch(id, server));
                return updatedServer != null ? Ok(mapper.Map<ServerDto>(updatedServer)) : NotFound();
            }
            catch (ServerConflictException ex) {
                return Conflict(ex.Message);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ServerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id) {
            var deletedServer = serverService.DeleteServer(id);
            return deletedServer != null ? Ok(mapper.Map<ServerDto>(deletedServer)) : NotFound();
        }

        [HttpGet("{serverId}/players")]
        [ProducesResponseType(typeof(IEnumerable<PlayerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult GetServerPlayers(int serverId)
        {
            try
            {
                var players = serverService.GetServerPlayers(serverId);
                return Ok(mapper.Map<IEnumerable<PlayerDto>>(players));
            }
            catch (UserNotExistsException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
