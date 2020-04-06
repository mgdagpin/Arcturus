using Canary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Canary.Infrastructure.Persistence.Configurations
{
    public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.Property(a => a.Title).HasMaxLength(150).IsRequired();
            builder.Property(a => a.Description).HasMaxLength(250);
            builder.Property(a => a.Content).HasMaxLength(250).IsRequired();
            builder.Property(a => a.PublishedOn).IsRequired();

            builder.HasOne(a => a.Author)
                .WithMany(a => a.BlogPosts);
        }
    }
}
