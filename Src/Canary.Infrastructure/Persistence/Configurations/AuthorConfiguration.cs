using Canary.Domain.Entities;
using System;

namespace Canary.Infrastructure.Persistence.Configurations
{
    public class AuthorConfiguration : BaseConfiguration<Author>
    {
        public override void ConfigureProperty(BasePropertyBuilder<Author> builder)
        {
            throw new NotImplementedException();
        }

        public override void SeedData(BaseSeeder<Author> builder)
        {
            builder.HasData(new Author
            {
                ID = -1
            });
        }
    }
}
