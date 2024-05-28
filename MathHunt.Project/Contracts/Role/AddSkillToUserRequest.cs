namespace MathHunt.Contracts.Role;

public record AddSkillToUserRequest(
    string emailId,
    string skillName
    );