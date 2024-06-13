namespace MathHunt.DataAccess.Entities;

public class UserSkillEntity
{
    public string AppUserId { get; set; }
    public AppUserEntity AppUserEntity { get; set; }

    public Guid SkillId { get; set; }
    public SkillEntity SkillEntity { get; set; }

    public string? ProficiencyLevel  { get; set; }
}