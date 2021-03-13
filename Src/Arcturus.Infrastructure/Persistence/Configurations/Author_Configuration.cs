using Arcturus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcturus.Infrastructure.Persistence.Configurations
{
    public partial class Author_Configuration : BaseConfiguration<Author>
    {
        protected override void ConfigureRelationship(BaseRelationshipBuilder<Author> builder)
        {
            builder.HasOne(a => a.User)
                .WithOne(a => a.Author)
                .HasForeignKey<Author>("ID")
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
        }

        protected override void SeedData(BaseSeeder<Author> builder)
        {
            builder.HasData(new
            {
                ID = -1
            });
        }
    }
}
