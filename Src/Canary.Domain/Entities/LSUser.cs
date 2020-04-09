using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Canary.Domain.Entities
{
    [Table("LSUser")]
    public class LSUser
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
    }
}
