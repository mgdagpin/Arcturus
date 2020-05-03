using Canary.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Canary.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : BaseConfiguration<User>
    {

        public override void ConfigureProperty(BasePropertyBuilder<User> builder)
        {
            builder.Property(a => a.FirstName).HasMaxLength(150).IsRequired();
            builder.Property(a => a.LastName).HasMaxLength(150).IsRequired();

            builder.Property(a => a.Gender)
              .HasConversion<string>()
              .HasMaxLength(30);

            builder.Property(a => a.FullName)
                .HasComputedColumnSql("CONCAT(LastName, ', ', FirstName, ISNULL(' ' + SUBSTRING(MiddleName,1,1) + '.', ''))");
        }

        public override void SeedData(BaseSeeder<User> builder)
        {
            builder.HasData(new User
            {
                ID = -1,
                FirstName = "Default",
                LastName = "Default"
            });
        }
    }
}
