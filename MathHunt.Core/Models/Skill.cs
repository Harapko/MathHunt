using Microsoft.AspNetCore.Identity;

namespace MathHunt.Core.Models;

public class Skill
{
    private Skill()
    {
        
    }
    private Skill(Guid id, string skillName, List<UserSkill> userSkills)
    {
        Id = id;
        SkillName = skillName;
        UserSkills = userSkills;

    }
    
    public Guid Id { get;  }
    public string SkillName { get; } = string.Empty;
    public List<UserSkill> UserSkills { get; } = [];

    public List<CompanySkill> CompanySkills { get; }

    public static (Skill userSkill, string Error) Create(Guid id, string skillName, List<UserSkill> identityUsers)
    {
        var error = string.Empty;

        if (string.IsNullOrWhiteSpace(skillName))
        {
            error = "Skill must have a name";
        }

        var userSkill = new Skill(id, skillName, identityUsers);
        return (userSkill, error);
    }
}