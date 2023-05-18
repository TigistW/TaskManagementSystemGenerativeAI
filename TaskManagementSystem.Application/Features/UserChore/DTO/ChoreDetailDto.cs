namespace TaskManagementSystem.Application.Features.UserChore.DTO;
using TaskManagementSystem.Domain.Common;
public class ChoreDetailDto
{
    public string Title { get; set; }
    public string Description { get; set; } = " ";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Status Status { get; set; }
}
