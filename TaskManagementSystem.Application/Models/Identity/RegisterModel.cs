using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Application.Models.Identity;

public class RegisterModel
{
    [Required]
    [EmailAddress]
    public string Email {get;set;} = "" ;
    [Required]
    public string FirstName { get; set; } = "";
    [Required]
    public string LastName { get; set; } = "";

    [Required]
    public string Password { get; set; } = "";

    [Required]
    public ICollection<string> Role {get; set;}


}
