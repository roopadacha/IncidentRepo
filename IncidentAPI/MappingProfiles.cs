using AutoMapper;
using DomainDto;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncidentAPI
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<IncidentDto, Incident>().ReverseMap();
            CreateMap<IncidentTypeDto, IncidentType>().ReverseMap();
        }        
    }
}
