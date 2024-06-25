namespace MathHunt.Contracts.Identity;

public record GETUserByIdResponse(
    string id,
    string name,
    string surname,
    string email,
    string phoneNumber,
    string englishLevel,
    string gitHubLink,
    string descriptionSkill,
    string role,
    string? photoPath,
    DateTime? lockEnd,
    bool isLock
    );