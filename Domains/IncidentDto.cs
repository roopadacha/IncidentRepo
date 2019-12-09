using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainDto
{
    public class IncidentDto : BaseEntityDto
    {
        [Required]
        public DateTimeOffset IncidentDate { get; set; }
        [Required]
        public DateTimeOffset IncidentTime { get; set; }
        [Required]
        [StringLength(250,ErrorMessage = "Description cannot exceed 250 characters")]
        public string Description { get; set; }
        [StringLength(50, ErrorMessage = "Person name cannot exceed 50 characters")]
        public string Person { get; set; }
        [Required]
        public int IncidentTypeId { get; set; }
        public IncidentTypeDto IncidentType { get; set; }
    }
}
