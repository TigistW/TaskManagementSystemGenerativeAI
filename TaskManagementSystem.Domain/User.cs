using System.ComponentModel.DataAnnotations;
namespace TaskManagementSystem.Domain;


public enum UserRole{
    Admin, User
}
public class User
{

    public int Id {get; set;}
    public string FirstName {get; set;} = "";

    public string LastName {get; set;} = "";

    public string Email {get; set;} = "";

    public string Password {get; set;} = "";

    public string AccountId {get; set;}
}
