namespace MathHunt.Contracts.Role;

public record AddSkillToUserRequest(
    string userName,
    string skillName
    );