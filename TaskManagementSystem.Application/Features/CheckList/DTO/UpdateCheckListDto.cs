using TaskManagementSystem.Domain.Common;

namespace TaskManagementSystem.Application.Features.CheckList.DTO;

public class UpdateCheckListDto
{

    public int id {get; set;}
    public string Title { get; set; } = " ";
    public string Description { get; set; } = " ";
    public Status Status { get; set; }
}
