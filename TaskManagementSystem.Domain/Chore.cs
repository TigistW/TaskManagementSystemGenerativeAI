using TaskManagementSystem.Domain.Common;

namespace TaskManagementSystem.Domain;

public class Chore : BaseDomainEntity
{
    public string Title { get; set; }
    public string Description { get; set; } = " ";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Status Status { get; set; }

}

