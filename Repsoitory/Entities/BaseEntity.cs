using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        [MaxLength(50)]
        public string CreatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        [MaxLength(50)]
        public string LastUpdatedBy { get; set; }
    }
}
