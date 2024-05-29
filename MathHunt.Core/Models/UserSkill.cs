using Microsoft.AspNetCore.Identity;

namespace MathHunt.Core.Models;

public class UserSkill
{
    private UserSkill()
    {
        
    }
    private UserSkill(Guid id, string skillName)
    {
        Id = id;
        SkillName = skillName;
        
    }
    
    public Guid Id { get;  }
    public string SkillName { get; } = string.Empty;
    public List<AppUser> IdentityUsers { get; } = [];

    public static (UserSkill userSkill, string Error) Create(Guid id, string skillName)
    {
        var error = string.Empty;

        if (string.IsNullOrWhiteSpace(skillName))
        {
            error = "Skill must have a name";
        }

        var userSkill = new UserSkill(id, skillName);
        return (userSkill, error);
    }
}