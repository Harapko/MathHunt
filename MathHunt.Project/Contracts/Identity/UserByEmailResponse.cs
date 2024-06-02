namespace MathHunt.Contracts.Identity;

public record UserByEmailResponse(
    string name,
    string surname,
    string email,
    string phoneNumber,
    string role
    );