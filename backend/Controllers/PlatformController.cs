using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServerING.DTO;
using ServerING.Exceptions;
using ServerING.Models;
using ServerING.Services;
using ServerING.ModelConverters;
using AutoMapper;
using ServerING.ModelsBL;
using Microsoft.AspNetCore.Cors;

namespace ServerING.Controllers {
    [EnableCors("MyPolicy")]
    [ApiController]
    [Route("/api/v1/platforms")]
    public class PlatformController : Controller {

        private readonly IMapper mapper;
        private readonly IPlatformService platformService;
        private readonly PlatformConverters platformConverters;

        public PlatformController(IPlatformService platformService, PlatformConverters platformConverters, IMapper mapper) {
            this.platformService = platformService;
            this.platformConverters = platformConverters;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PlatformDto>), StatusCodes.Status200OK)]
        public IActionResult GetAll() {
            return Ok(mapper.Map<IEnumerable<PlatformDto>>(platformService.GetAllPlatforms()));
        }

        [HttpPost]
        [ProducesResponseType(typeof(PlatformDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Add(PlatformBaseDto platform) {
            try {
                var addedPlatform = platformService
                    .AddPlatform(mapper.Map<PlatformBL>(platform));
                return Ok(mapper.Map<PlatformDto>(addedPlatform));
            }
            catch (PlatformConflictException ex) {
                return Conflict(ex.Message);
            }
        }

        [EnableCors("MyPolicy")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PlatformDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id) {
            var platform = mapper.Map<PlatformDto>(platformService.GetPlatformByID(id));
            return platform != null ? Ok(platform) : NotFound();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PlatformDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Put(int id, PlatformBaseDto platform) {
            try {
                var updatedPlatform = platformService
                    .UpdatePlatform(id, mapper.Map<PlatformBL>(platform,
                        o => o.AfterMap((src, dest) => dest.Id = id)
                    ));
                return updatedPlatform != null ? Ok(mapper.Map<PlatformDto>(updatedPlatform)) : NotFound();
            }
            catch (PlatformConflictException ex) {
                return Conflict(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(PlatformDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Patch(int id, PlatformBaseDto platform) {
            try {
                var updatedPlatform = platformService
                    .UpdatePlatform(id, platformConverters.convertPatch(id, platform));
                return updatedPlatform != null ? Ok(mapper.Map<PlatformDto>(updatedPlatform)) : NotFound();
            }
            catch (PlatformConflictException ex) {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(PlatformDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id) {
            var deletedPlatform = platformService
                .DeletePlatform(id);
            return deletedPlatform != null ? Ok(mapper.Map<PlatformDto>(deletedPlatform)) : NotFound();
        }
    }
}
