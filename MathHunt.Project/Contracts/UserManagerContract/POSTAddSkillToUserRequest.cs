namespace MathHunt.Contracts.Role;

public record POSTAddSkillToUserRequest(
    string userName,
    string skillName,
    string proficiencyLevel
    );