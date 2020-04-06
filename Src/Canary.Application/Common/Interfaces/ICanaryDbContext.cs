using Canary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Canary.Application
{
    public interface ICanaryDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<BlogPost> BlogPosts { get; set; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
