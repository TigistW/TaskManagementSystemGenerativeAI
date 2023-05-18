using TaskManagementSystem.Domain.Common;

namespace TaskManagementSystem.Application.Features.UserChore.DTO;

public class UpdateChoreDto
{
    public string Title { get; set; }
    public string Description { get; set; } = " ";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Status Status { get; set; }
}

