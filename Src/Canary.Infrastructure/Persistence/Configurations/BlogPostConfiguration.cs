using Canary.Domain.Entities;

namespace Canary.Infrastructure.Persistence.Configurations
{
    public class BlogPostConfiguration : BaseConfiguration<BlogPost>
    {
        public override void ConfigureProperty(BasePropertyBuilder<BlogPost> builder)
        {
            builder.Property(a => a.Title).HasMaxLength(150).IsRequired();
            builder.Property(a => a.Description).HasMaxLength(250);
            builder.Property(a => a.Content).HasMaxLength(250).IsRequired();
            builder.Property(a => a.PublishedOn).IsRequired();
        }

        public override void ConfigureRelationship(BaseRelationshipBuilder<BlogPost> builder)
        {
            builder.HasOne(a => a.Author)
                .WithMany(a => a.BlogPosts);
        }
    }
}
