using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Entities
{
    public class Incident :  BaseEntity
    {
        [Required]
        public DateTimeOffset IncidentDate { get; set; }
        [Required]
        public DateTimeOffset IncidentTime { get; set; }
        [Required]
        [MaxLength(250)]
        public string Description { get; set; }
        [MaxLength(50)]
        public string Person { get; set; }
        [Required]
        public int IncidentTypeId { get; set; }
        [ForeignKey("IncidentTypeId")]
        public IncidentType IncidentType { get; set; }
    }
}
