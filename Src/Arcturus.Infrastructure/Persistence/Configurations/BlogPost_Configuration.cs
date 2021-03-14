using Arcturus.Domain.Entities;

namespace Arcturus.Infrastructure.Persistence.Configurations
{
    public partial class BlogPost_Configuration : BaseConfiguration<BlogPost>
    {
        protected override void ConfigureProperty(BasePropertyBuilder<BlogPost> builder)
        {
            builder.Property(a => a.Title).HasMaxLength(150).IsRequired();
            builder.Property(a => a.Description).HasMaxLength(250);
            builder.Property(a => a.Content).HasMaxLength(250).IsRequired();
            builder.Property(a => a.PublishedOn).IsRequired();
        }

        protected override void ConfigureRelationship(BaseRelationshipBuilder<BlogPost> builder)
        {
            builder.HasOne(a => a.Author)
                .WithMany(a => a.BlogPosts);
        }
    }
}
