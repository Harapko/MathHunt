using Microsoft.AspNetCore.Identity;

namespace MathHunt.Core.Models;

public class AppUser : IdentityUser
{
    public string? UserSurname { get; set; }
    public List<UserSkill>? UserSkills { get; set; }
}