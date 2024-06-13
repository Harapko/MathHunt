namespace MathHunt.Core.Models;

public class UserSkill
{
    private UserSkill()
    {
        
    }
    
    private UserSkill(string appUserId, Guid skillId, string proficiencyLevel, AppUser appUser, Skill skill)
    {
        AppUserId = appUserId;
        SkillId = skillId;
        ProficiencyLevel = proficiencyLevel;
        AppUser = appUser;
        Skill = skill;
    }
    
    public string AppUserId { get; }
    public AppUser AppUser { get; }

    public Guid SkillId { get; }
    public Skill Skill { get; }

    public string? ProficiencyLevel  { get; }

    public static (UserSkill userSkill, string Error) Create(string appUserId, Guid skillId, string proficiencyLevel, AppUser appUser, Skill skill)
    {
        var error = string.Empty;
        if (string.IsNullOrWhiteSpace(appUserId))
        {
            error = "AppUserId is null";
        }
        
        if (string.IsNullOrWhiteSpace(skillId.ToString()))
        {
            error = "AppUserId is null";
        }

        var userSkill = new UserSkill(appUserId, skillId, proficiencyLevel, appUser, skill);

        return (userSkill, error);
    }
}