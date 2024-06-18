using MathHunt.Core.Models;

namespace MathHunt.Contracts.Identity;

public record GETCurrentUserResponse(
    string id,
    string name,
    string surname,
    string email,
    string phoneNumber,
    string englishLevel,
    string gitHubLink,
    string descriptionSkill,
    string role,
    string photoPath,
    Company[] companyList,
    GETUserSkillResponse[] skillName
    );