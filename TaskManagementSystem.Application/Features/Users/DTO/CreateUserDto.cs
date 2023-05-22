
namespace TaskManagementSystem.Application.Features.Users.DTO;

public class CreateUserDto : UserDto
{
    public int Id { get ; set ;}
    public string FirstName { get; set; }
    public string LastName { get;set; }
    public string Email { get;set; }
    public string Password { get;set; }
    public string AccountId { get;set; }
}
