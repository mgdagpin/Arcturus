using Canary.Domain;
using Canary.Domain.Entities;
using System.Collections;
using System.Collections.Generic;

namespace Canary.Application
{
    public class UserBO : IMapFrom<User>
    {
        public int UserID { get; set; }
        public string FullName { get; set; }

        public Gender Gender { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
