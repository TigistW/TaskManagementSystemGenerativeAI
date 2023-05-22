namespace TaskManagementSystem.Application.Models.Identity;

public class JwtSettings
{
    public string Issuer {get; set;} = "";
    public string Audience {get; set;} = "";
    public string Key {get; set;}= "";
    public double DurationInMinutes {get; set;} = 0.0;

}
