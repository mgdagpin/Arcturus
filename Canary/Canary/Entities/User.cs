using System.ComponentModel.DataAnnotations.Schema;

namespace Canary.Domain.Entities
{
    public class User : AuditedBaseEntity
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string FullName { get; set; }

        public Gender Gender { get; set; }

        [InverseProperty("User")]
        public Author Author { get; set; }
    }
}
