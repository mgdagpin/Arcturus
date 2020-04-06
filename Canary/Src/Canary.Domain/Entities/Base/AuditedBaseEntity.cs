using System;
using System.ComponentModel.DataAnnotations;

namespace Canary.Domain
{
    public abstract class AuditedBaseEntity : BaseEntity
    {
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }

        //public DateTime? ModifiedOn { get; set; }
        //public string ModifiedBy { get; set; }


        //[Timestamp]
        //public byte[] LastChanged { get; set; }
    }
}
