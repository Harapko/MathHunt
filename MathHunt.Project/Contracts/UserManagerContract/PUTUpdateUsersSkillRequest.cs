namespace MathHunt.Contracts.Role;

public record PUTUpdateUsersSkillRequest(
    string userId,
    Guid oldSkillId,
    Guid newSkillId,
    string proficiencyLevel
);
