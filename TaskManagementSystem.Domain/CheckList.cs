using TaskManagementSystem.Domain.Common;

namespace TaskManagementSystem.Domain;

public class CheckList
{
    public string Title { get; set; }
    public string Description { get; set; } = " ";
    public int ChoreId {get; set;}
    public Status Status { get; set; }
}
