using TaskManagementSystem.Application.Responses;

namespace TaskManagementSystem.Application.Models.Identity;
public interface IAuthService
{
    public Task<BaseCommandResponse<RegisterResponse>> Register(RegisterModel request);

    public Task<BaseCommandResponse<LoginResponse>> Login(LoginModel request);

    public Task<bool> DeleteUser(string Email);

}