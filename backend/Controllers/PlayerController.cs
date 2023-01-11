using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ServerING.DTO;
using ServerING.Exceptions;
using ServerING.ModelConverters;
using ServerING.Models;
using ServerING.ModelsBL;
using ServerING.Services;

namespace ServerING.Controllers {
    [EnableCors("MyPolicy")]
    [ApiController]   
    [Route("/api/v1/players")]
    public class PlayerController : Controller {
        private readonly IPlayerService playerService;
        private readonly IMapper mapper;
        private readonly PlayerConverters playerConverters;

        public PlayerController(IPlayerService playerService,
            IMapper mapper, PlayerConverters playerConverters) {
            this.playerService = playerService;
            this.mapper = mapper;
            this.playerConverters = playerConverters;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PlayerDto>), StatusCodes.Status200OK)]
        public IActionResult GetAll() {
            return Ok(mapper.Map<IEnumerable<PlayerDto>>(playerService.GetAllPlayers()));
        }

        [HttpPost]
        [ProducesResponseType(typeof(PlayerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Add(PlayerBaseDto player) {
            try {
                var addedPlayer = playerService
                    .AddPlayer(mapper.Map<PlayerBL>(player));
                return Ok(mapper.Map<PlayerDto>(addedPlayer));
            }
            catch (PlayerAlreadyExistsException ex) {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PlayerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id) {
            var player = mapper.Map<PlayerDto>(playerService.GetPlayerByID(id));
            return player != null ? Ok(player) : NotFound();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PlayerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Put(int id, PlayerBaseDto player) {
            try {
                var updatedPlayer = playerService
                    .UpdatePlayer(id, mapper.Map<PlayerBL>(player,
                        o => o.AfterMap((src, dest) => dest.Id = id)));

                return updatedPlayer != null ? Ok(mapper.Map<PlayerDto>(updatedPlayer)) : NotFound();
            }
            catch (PlayerAlreadyExistsException ex) {
                return Conflict(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(PlayerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Patch(int id, PlayerBaseDto player) {
            try {
                var updatedPlayer = playerService
                    .UpdatePlayer(id, playerConverters.convertPatch(id, player));
                return updatedPlayer != null ? Ok(mapper.Map<PlayerDto>(updatedPlayer)) : NotFound();
            }
            catch (PlayerAlreadyExistsException ex) {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(PlayerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var deletedPlayer = playerService
                .DeletePlayer(id);
            return deletedPlayer != null ? Ok(mapper.Map<PlayerDto>(deletedPlayer)) : NotFound();
        }

    }
}