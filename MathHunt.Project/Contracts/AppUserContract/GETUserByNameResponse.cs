namespace MathHunt.Contracts.Identity;

public record GETUserByNameResponse(
    string id,
    string name,
    string surname,
    string email,
    string phoneNumber,
    string englishLevel,
    string descriptionSkill,
    string role,
    string[] skillName
    );