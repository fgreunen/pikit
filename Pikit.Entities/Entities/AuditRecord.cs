using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Entities.Entities
{
    public class AuditRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AuditType { get; set; }

        [Required]
        public string AuditUser { get; set; }

        [Required]
        public DateTime AuditDate { get; set; }

        [Required]
        public int RecordId { get; set; }

        [Required]
        public string RecordType { get; set; }

        [Required]
        public string RecordData { get; set; }
    }
}