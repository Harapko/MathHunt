namespace MathHunt.Contracts.Identity;

public record AllUserResponse(
    string name,
    string surname,
    string email,
    string phoneNumber,
    string englishLevel,
    string role,
    string[] skillName
    );