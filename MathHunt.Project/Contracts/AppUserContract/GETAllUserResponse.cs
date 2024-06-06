namespace MathHunt.Contracts.Identity;

public record GETAllUserResponse(
    string name,
    string surname,
    string email,
    string phoneNumber,
    string englishLevel,
    string descriptionSkill,
    string role,
    string[] skillName
    );