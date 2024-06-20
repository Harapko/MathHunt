namespace MathHunt.Contracts.Identity;

public record PUTUpdateUserRequest(
    // string userName,
    string userSurname,
    string email,
    string phoneNumber,
    string englishLevel,
    string gitHubLink,
    string descriptionSkill
    );