
using Microsoft.AspNetCore.Identity;

namespace MathHunt.Core.Models;

public sealed class AppUser
{
    private AppUser()
    {
        
    }
    private AppUser(string id, string userName, string? userSurname, string email, string? phoneNumber,
        string? englishLevel, string? descriptionSkill, string role, List<UserSkill>? userSkills, List<Company> companies)
    {
        Id = id;
        UserName = userName;
        UserSurname = userSurname;
        Email = email;
        PhoneNumber = phoneNumber;
        EnglishLevel = englishLevel;
        DescriptionSkill = descriptionSkill;
        Role = role;
        UserSkills = userSkills;
        Companies = companies;
    }

    public string Id { get; }
    public string UserName { get; } = string.Empty;
    public string? UserSurname { get; } = string.Empty;
    public string Email { get; } = string.Empty;
    public string? PhoneNumber { get; } = string.Empty;
    public string? EnglishLevel { get; } = string.Empty;
    public string? DescriptionSkill { get; } = string.Empty;
    public string Role { get; } = string.Empty;
    public List<UserSkill>? UserSkills { get; } = [];
    public List<Company> Companies { get; } = [];

    public static (AppUser appUser, string Error) Create(string id, string userName, string? userSurname, string email, string phoneNumber,
        string? englishLevel, string? descriptionSkill, string role, List<UserSkill>? userSkills, List<Company> companies)
    {
        var error = string.Empty;
        if (string.IsNullOrWhiteSpace(userName))
        {
            error = "User must have a name";
        }
        
        if (!string.IsNullOrWhiteSpace(email))
        {
            error = "Email is empty";
        }

        if (string.IsNullOrWhiteSpace(role))
        {
            error = "Role is null";
        }

        var appUser = new AppUser(id, userName, userSurname, email, phoneNumber, englishLevel, descriptionSkill, role, userSkills, companies);
        return  (appUser, error);
    }
}