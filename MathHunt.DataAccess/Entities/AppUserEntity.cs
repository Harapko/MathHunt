using MathHunt.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace MathHunt.DataAccess.Entities;

public class AppUserEntity : IdentityUser
{
    public string? UserSurname { get; set; } = string.Empty;
    public string? Role { get; set; } = string.Empty;
    public string? EnglishLevel { get; set; } = string.Empty;
    public string? DescriptionSkill { get; set; } = string.Empty;
    public string GitHubLink { get; set; } = string.Empty;
    public List<UserSkillEntity>? UserSkillsEntities { get; set; } = [];

    public List<CompanyEntity>? CompaniesEntity { get; set; } = [];

    public List<PhotoUserEntity> PhotoUserEntities { get; set; } = [];
}