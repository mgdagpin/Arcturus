using Arcturus.Domain;
using Arcturus.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arcturus.Infrastructure.Persistence.Configurations
{
    public partial class User_Configuration : BaseConfiguration<User>
    {

        protected override void ConfigureProperty(BasePropertyBuilder<User> builder)
        {
            builder.Property(a => a.FirstName).HasMaxLength(150).IsRequired();
            builder.Property(a => a.LastName).HasMaxLength(150).IsRequired();

            builder.Property(a => a.Gender)
              .HasConversion<string>()
              .HasMaxLength(30);

            builder.Property(a => a.FullName)
                .HasComputedColumnSql("CONCAT(LastName, ', ', FirstName, ISNULL(' ' + SUBSTRING(MiddleName,1,1) + '.', ''))");
        }

        protected override void SeedData(BaseSeeder<User> builder)
        {
            builder.HasData(new
            {
                ID = -1,
                FirstName = "Mar",
                LastName = "Dagpin",
                MiddleName = "",
                Gender = Gender.Male
            });
        }
    }
}
