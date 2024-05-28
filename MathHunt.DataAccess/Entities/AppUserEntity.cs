using MathHunt.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace MathHunt.DataAccess.Entities;

public class AppUserEntity : IdentityUser
{
    public string? UserSurname { get; set; }

    public List<UserSkillEntity>? UserSkillsEntities { get; set; } = [];
}