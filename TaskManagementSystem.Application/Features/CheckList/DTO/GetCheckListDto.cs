using TaskManagementSystem.Domain.Common;

namespace TaskManagementSystem.Application.Features.CheckList.DTO;

public class GetCheckListDto
{
    public string Title { get; set; }
    public string Description { get; set; } = " ";
    public int ChoreId { get; set; }
    public Status Status { get; set; }
}
