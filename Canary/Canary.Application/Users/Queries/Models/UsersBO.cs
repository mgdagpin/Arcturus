using Canary.Domain;
using Canary.Domain.Entities;

namespace Canary.Application.Authors.Queries.Models
{
    public class UsersBO : IMapFrom<User>
    {
        public string FullName { get; set; }

        public Gender Gender { get; set; }
    }
}
