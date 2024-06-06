
using Microsoft.AspNetCore.Identity;

namespace MathHunt.Core.Models;

public sealed class AppUser : IdentityUser
{
    private AppUser()
    {
        
    }
    private AppUser(string userName, string? userSurname, string? email, string? phoneNumber, string englishLevel, string role, List<UserSkill> userSkills)
    {
        
        UserName = userName;
        UserSurname = userSurname;
        Email = email;
        PhoneNumber = phoneNumber;
        EnglishLevel = englishLevel;
        Role = role;
        UserSkills = userSkills;
    }

    
    public string UserName { get; }
    public string? UserSurname { get; }
    public string? Email { get; }
    public string? PhoneNumber { get; }
    public string EnglishLevel { get; }
    public string Role { get; }
    public List<UserSkill>? UserSkills { get; } = [];

    public static (AppUser appUser, string Error) Create(string userName, string userSurname, string email, string phoneNumber, string englishLevel, string role, List<UserSkill> userSkills)
    {
        var error = string.Empty;
        if (!string.IsNullOrWhiteSpace(email))
        {
            error = "Email is empty";
        }

        var appUser = new AppUser(userName, userSurname, email, phoneNumber, englishLevel, role, userSkills);
        return  (appUser, error);
    }
}