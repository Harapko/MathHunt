using MathHunt.Core.Models;

namespace MathHunt.DataAccess.Entities;

public class UserSkillEntity
{
    public Guid Id { get; set; }
    public string SkillName { get; set; } = string.Empty;
    public List<AppUserEntity> AppUserEntities { get; set; } = [];
}