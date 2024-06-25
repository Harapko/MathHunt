namespace MathHunt.Contracts.Skill;

public record GETUsersBySkillResponse(
    string id,
    string userName,
    string proficiencyLevel
    );