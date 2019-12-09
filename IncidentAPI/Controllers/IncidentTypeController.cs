using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DomainDto;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Entities;

namespace IncidentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentTypeController : ControllerBase
    {
        private IGenericRepository<IncidentType> repository;
        private readonly IMapper mapper;

        public IncidentTypeController(IGenericRepository<IncidentType> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<IncidentTypeDto>> Get()
        {
            var incidentTypes = this.repository.GetAllAsync().Result;
            return Ok(mapper.Map<List<IncidentTypeDto>>(incidentTypes.ToList()));
        }
    }
}