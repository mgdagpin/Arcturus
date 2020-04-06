using Canary.Application;
using Canary.Domain;
using Canary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Canary.Infrastructure.Persistence
{
    public class CanaryDbContext : DbContext, ICanaryDbContext
    {
        private readonly IDateTime dateTime;

        public CanaryDbContext(DbContextOptions<CanaryDbContext> dbContextOptions, IDateTime dateTime) : base(dbContextOptions)
        {
            this.dateTime = dateTime;
        }



        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditedBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        // entry.Entity.CreatedOn = dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        }
    }
}
