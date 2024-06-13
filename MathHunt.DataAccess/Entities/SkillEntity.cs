using MathHunt.Core.Models;

namespace MathHunt.DataAccess.Entities;

public class SkillEntity
{
    public Guid Id { get; set; }
    public string SkillName { get; set; } = string.Empty;
    public List<UserSkillEntity> UserSkillEntities { get; set; } = [];

    public List<CompanySkillEntity> CompanySkill { get; set; } = [];
}