using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using ServerING.DTO;
using ServerING.Exceptions;
using ServerING.ModelConverters;
using ServerING.Models;
using ServerING.ModelsBL;
using ServerING.Services;

namespace ServerING.Controllers {
    [EnableCors("MyPolicy")]
    [ApiController]
    [Route("/api/v1/hostings")]
    public class HostingController : Controller {
        private readonly IHostingService hostingService;
        private readonly ILogger<HostingController> _logger;
        private readonly IMapper mapper;
        private readonly HostingConverters hostingConverters;

        public HostingController(IHostingService hostingService, ILogger<HostingController> logger,
            IMapper mapper, HostingConverters hostingConverters) {
            this.hostingService = hostingService;
            this.mapper = mapper;
            this.hostingConverters = hostingConverters;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<HostingDto>), StatusCodes.Status200OK)]
        public IActionResult GetAll() {
            _logger.LogInformation("hostings: Request: GET");
            return Ok(mapper.Map<IEnumerable<HostingDto>>(hostingService.GetAllHostings()));
        }

        [HttpPost]
        [ProducesResponseType(typeof(HostingDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Add(HostingBaseDto hosting) {
            try {
                var addedHosting = hostingService
                    .AddHosting(mapper.Map<WebHostingBL>(hosting));
                return Ok(mapper.Map<HostingDto>(addedHosting));
            }
            catch (HostingConflictException ex) {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(HostingDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id) {
            var hosting = mapper.Map<HostingDto>(hostingService.GetHostingByID(id));
            return hosting != null ? Ok(hosting) : NotFound();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(HostingDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Put(int id, HostingBaseDto hosting) {
            try {
                var updatedHosting = hostingService
                    .UpdateHosting(id, mapper.Map<WebHostingBL>(hosting,
                        o => o.AfterMap((src, dest) => dest.Id = id)));

                return updatedHosting != null ? Ok(mapper.Map<HostingDto>(updatedHosting)) : NotFound();
            }
            catch (HostingConflictException ex) {
                return Conflict(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(HostingDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Patch(int id, HostingBaseDto hosting) {
            try {
                var updatedHosting = hostingService
                    .UpdateHosting(id, hostingConverters.convertPatch(id, hosting));
                return updatedHosting != null ? Ok(mapper.Map<HostingDto>(updatedHosting)) : NotFound();
            }
            catch (HostingConflictException ex) {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(HostingDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id) {
            var deletedHosting = hostingService
                .DeleteHosting(id);
            return deletedHosting != null ? Ok(mapper.Map<HostingDto>(deletedHosting)) : NotFound();
        }
    }
}
