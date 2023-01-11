using System.Collections.Generic;
using System.Linq;
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
    [Route("/api/v1/countries")]
    public class CountryController : Controller {
        private readonly IMapper mapper;
        private readonly CountryConverters countryConverters;
        private readonly ICountryService countryService;

        public CountryController(ICountryService countryService, 
            IMapper mapper, CountryConverters countryConverters) {
            this.countryService = countryService;
            this.mapper = mapper;
            this.countryConverters = countryConverters;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CountryDto>), StatusCodes.Status200OK)]
        public IActionResult GetAll() {
            return Ok(mapper.Map<IEnumerable<CountryDto>>(countryService.GetAllCountries()));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CountryDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Add(CountryBaseDto country) {
            try {
                var addedCountry = countryService
                    .AddCountry(mapper.Map<CountryBL>(country));
                return Ok(mapper.Map<CountryDto>(addedCountry));
            }
            catch (CountryAlreadyExistsException ex) {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CountryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var country = mapper.Map<CountryDto>(countryService.GetCountryByID(id));
            return country != null ? Ok(country) : NotFound();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CountryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Put(int id, CountryBaseDto country) {
            try {
                var updatedCountry = countryService
                    .UpdateCountry(id, mapper.Map<CountryBL>(country,
                        o => o.AfterMap((src, dest) => dest.Id = id)));

                return updatedCountry != null ? Ok(mapper.Map<CountryDto>(updatedCountry)) : NotFound();
            }
            catch (CountryAlreadyExistsException ex) {
                return Conflict(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(CountryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Patch(int id, CountryBaseDto country) {
            try {
                var updatedCountry = countryService
                    .UpdateCountry(id, countryConverters.convertPatch(id, country));
                return updatedCountry != null ? Ok(mapper.Map<CountryDto>(updatedCountry)) : NotFound();
            }
            catch (CountryAlreadyExistsException ex) {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CountryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id) {
            var deletedCountry = countryService
                .DeleteCountry(id);
            return deletedCountry != null ? Ok(mapper.Map<CountryDto>(deletedCountry)) : NotFound();
        }

    }
}