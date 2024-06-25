namespace MathHunt.Contracts.Identity;

public record GETUserSkillResponse(
    Guid id,
    string skillName,
    string proficiencyLevel
);