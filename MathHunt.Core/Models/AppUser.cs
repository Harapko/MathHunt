
using Microsoft.AspNetCore.Identity;

namespace MathHunt.Core.Models;

public sealed class AppUser : IdentityUser
{
    public AppUser()
    {
        
    }
    private AppUser(string id ,string  userName, string userSurname, string email, string phoneNumber)
    {
        Id = id;
        UserName = userName;
        UserSurname = userSurname;
        Email = email;
        PhoneNumber = phoneNumber;
    }
    
    public string? UserSurname { get; set; }
    public List<UserSkill>? UserSkills { get; set; } = [];

    public static (AppUser appUser, string Error) Create(string id ,string  userName, string userSurname, string email, string phoneNumber)
    {
        var error = string.Empty;
        if (!string.IsNullOrWhiteSpace(email))
        {
            error = "Email is empty";
        }

        var appUser = new AppUser(id, userName, userSurname, email, phoneNumber);
        return  (appUser, error);
    }
}