

using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskManagementSystem.Application.Models.Identity;
using TaskManagementSystem.Application.Responses;
using TaskManagementSystem.Identity.Models;

namespace TaskManagementSystem.Identity.Services;


public class AuthService : IAuthService
{

    private readonly UserManager<UserIdentity> _userManager;
    private readonly SignInManager<UserIdentity> _signInManager;
    private readonly ServerSettings _serverSettings;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        UserManager<UserIdentity> userManager,
                       SignInManager<UserIdentity> signInManager,
                       IOptions<JwtSettings> jwtSettings,
                       IOptions<ServerSettings> serverSettings
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSettings = jwtSettings.Value;
        _serverSettings = serverSettings.Value;

    }


    public Task<bool> DeleteUser(string Email)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseCommandResponse<LoginResponse>> Login(LoginModel request)
    {
        BaseCommandResponse<LoginResponse> result = new BaseCommandResponse<LoginResponse>();
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            result.Success = false;
            result.Errors.Add($"User with given Email({request.Email}) doesn't exist");
            return result;
        }

        var res = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
        if (!res.Succeeded)
        {
            result.Success = false;
            result.Errors.Add($"Incorrect password");
            return result;
        }

        JwtSecurityToken token = await GenerateToken(user);

        var response = new LoginResponse()
        {
            UserId = user.Id,
            FirstName = user.UserName,
            Email = user.Email,
            Token = new JwtSecurityTokenHandler().WriteToken(token)
        };

        result.Value = response;
        return result;

    }

    private async Task<JwtSecurityToken> GenerateToken(UserIdentity user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();
        foreach (var role in roles)
        {
            roleClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var Claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id)
        }.Union(userClaims)
         .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredential = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: Claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredential
        );

        return token;
    }

    public async Task<BaseCommandResponse<RegisterResponse>> Register(RegisterModel request)
    {
        var result = new BaseCommandResponse<RegisterResponse>();
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            result.Success = false;
            result.Errors.Add($"User with given Email({request.Email}) already exists");
            return result;
        }

        var user = new UserIdentity
        {
            UserName = request.FirstName,
            Email = request.Email,
            EmailConfirmed = false
        };

        var createResult = await _userManager.CreateAsync(user, request.Password);

        if (!createResult.Succeeded)
        {
            result.Success = false;
            foreach (var Error in createResult.Errors)
            {
                result.Errors.Add(Error.Description);
            }
            return result;
        }

        var createdUser = await _userManager.FindByNameAsync(request.FirstName);
        await _userManager.AddToRolesAsync(createdUser, request.Role);

        result.Success = true;
        result.Value = new RegisterResponse
        {
            UserId = createdUser.Id,
            Email = createdUser.Email
        };

        return result;



    }
}
