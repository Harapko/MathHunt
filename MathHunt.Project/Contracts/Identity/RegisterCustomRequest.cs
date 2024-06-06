namespace MathHunt.Contracts.Identity;

public record RegisterCustomRequest(
    string name,
    string surname,
    string email,
    string phoneNumber,
    string englishLevel,
    string password,
    string role
);
