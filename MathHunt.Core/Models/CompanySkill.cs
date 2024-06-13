namespace MathHunt.Core.Models;

public class CompanySkill
{
    private CompanySkill()
    {
        
    }

    private CompanySkill(Guid companyId, Company company, Guid skillId, Skill skill)
    {
        CompanyId = companyId;
        Company = company;
        SkillId = skillId;
        Skill = skill;
    }
    public Guid CompanyId { get; }
    public Company Company { get; }

    public Guid SkillId { get; }
    public Skill Skill { get; }

    public static (CompanySkill companySkill, string Error) Create(Guid companyId, Company company, Guid skillId, Skill skill)
    {
        var error = string.Empty;
        if (companyId == Guid.Empty)
        {
            error = "Company Id is empty";
        }
        
        if (skillId == Guid.Empty)
        {
            error = "Skill Id is empty";
        }

        var companySkill = new CompanySkill(companyId, company, skillId, skill);

        return (companySkill, error);
    }
}