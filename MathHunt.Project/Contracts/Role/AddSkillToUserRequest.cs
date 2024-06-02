namespace MathHunt.Contracts.Role;

public record AddSkillToUserRequest(
    string email,
    string skillName
    );