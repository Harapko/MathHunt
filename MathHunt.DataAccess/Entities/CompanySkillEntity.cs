namespace MathHunt.DataAccess.Entities;

public class CompanySkillEntity
{
    public Guid CompanyId { get; set; }
    public CompanyEntity Company { get; set; }

    public Guid SkillId { get; set; }
    public SkillEntity Skill { get; set; }
}