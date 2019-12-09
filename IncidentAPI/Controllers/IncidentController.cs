using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using DomainDto;
using IncidentAPI.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Entities;

namespace IncidentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        private IGenericRepository<Incident> repository;
        private readonly IMapper mapper;

        public IncidentController(IGenericRepository<Incident> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        
        /// <summary>
        /// Gets the List of Incidents.
        /// </summary>
        /// <returns>List of Incidents</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            var incidentsEntity = this.repository.GetAllWithChildAsync(q => q.IncidentType).Result;
            if (incidentsEntity == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<IncidentDto>>(incidentsEntity.ToList()));
        }

        /// <summary>
        /// Get the Incident by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Incident</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var incidentsEntity = this.repository.GetByIdWithChildAsync(id, q => q.IncidentType).Result;
            if (incidentsEntity == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<IncidentDto>(incidentsEntity));
        }
        
        /// <summary>
        /// Creates an Incident.
        /// </summary>
        /// <param name="incidentDto"></param>
        /// <returns>Incident</returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ServiceFilter(typeof(ValidateActionFilter))]
        public async Task<IActionResult> Post([FromBody]IncidentDto incidentDto)
        {    
            var incidentEntity = mapper.Map<Incident>(incidentDto);

            await this.repository.CreateAsync(incidentEntity);

            incidentDto = mapper.Map<IncidentDto>(incidentEntity);

            return CreatedAtAction(nameof(GetById), new { id = incidentDto.Id }, incidentDto);
        }

        /// <summary>
        /// Updates an Incident.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="incident"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ServiceFilter(typeof(ValidateActionFilter))]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Incident>))]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody] IncidentDto incident)
        {
            var incidentEntity = getIncident();
           
            mapper.Map(incident, incidentEntity);

            await this.repository.UpdateAsync(incidentEntity);


            return NoContent();
        }

        /// <summary>
        /// Deletes a specific Incident.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Incident>))]
        public async Task<IActionResult> Delete(int id)
        {
            await this.repository.DeleteAsync(getIncident());

            return NoContent();
        }

        private Incident getIncident()
        {
            return HttpContext.Items["entity"] as Incident;
        }
    }
}
