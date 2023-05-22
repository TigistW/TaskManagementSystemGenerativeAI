namespace TaskManagementSystem.Application.Features.Users.DTO;

public class GetUserByIdDto : UserDto
{
    public int Id { get;set; }
    public string FirstName {get;set; }
    public string LastName {get;set; }

    public string Email {get;set ;}
}
