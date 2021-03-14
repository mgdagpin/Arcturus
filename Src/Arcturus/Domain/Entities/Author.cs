using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arcturus.Domain.Entities
{

    public class Author : AuditedBaseEntity
    {
        public int ID { get; set; }
        public User User { get; set; }

        public ICollection<BlogPost> BlogPosts { get; set; } = new HashSet<BlogPost>();
    }
}
