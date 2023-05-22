using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Persistence.Configurations.Entities;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(
            new User
            {
                Id = 1,
                FirstName = "Abebe",
                LastName = "kebede",
                AccountId = "abe1"
            },

            new User
            {
                Id = 2,
                FirstName = "Alemu",
                LastName = "Lula",
                AccountId = "Lula1"
            }
            );
    }

}