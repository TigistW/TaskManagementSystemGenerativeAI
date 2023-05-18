using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Persistence.Configurations.Entities;

public class ChoreConfigurations : IEntityTypeConfiguration<Chore>
{
    public void Configure(EntityTypeBuilder<Chore> builder){
        
    }
}
