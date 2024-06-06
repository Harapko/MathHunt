namespace MathHunt.Contracts.Identity;

public record UserByNameResponse(
    string name,
    string surname,
    string email,
    string phoneNumber,
    string role,
    string[] skillName
    );