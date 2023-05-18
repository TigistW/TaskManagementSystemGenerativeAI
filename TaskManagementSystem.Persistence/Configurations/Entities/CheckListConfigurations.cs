using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Persistence.Configurations.Entities;
public class CheckListConfigurations: IEntityTypeConfiguration<CheckList>
{
    public void Configure(EntityTypeBuilder<CheckList> builder)
    {
        
    }
}
