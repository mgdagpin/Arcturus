using Canary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Canary.Infrastructure.Persistence.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasData(new Author
            {
                ID = -1,
                CreatedOn = new DateTime(2020, 1, 1)
            });
        }
    }
}
