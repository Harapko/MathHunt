namespace MathHunt.Contracts.Role;

public record DELETEUserSkillRequest(
    string userId,
    string skillName
    );