using System;
using System.ComponentModel.DataAnnotations;

namespace DomainDto
{
    public class IncidentTypeDto : BaseEntityDto
    {
        [Required]
        public string Name { get; set; }
    }
}
