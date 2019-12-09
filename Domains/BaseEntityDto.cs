using System;
using System.Collections.Generic;
using System.Text;

namespace DomainDto
{
    public class BaseEntityDto
    {
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
