namespace MathHunt.Contracts.Identity;

public record LoginCustomRequest(
    string email,
    string password,
    bool rememberMe
    );