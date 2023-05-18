namespace TaskManagementSystem.Domain.Common;

public enum Status
{
    Completed,
    Incompleted
}

public enum Role
{
    Admin,
    User
}
public class BaseDomainEntity
{
    public int Id {get; set;}
}
