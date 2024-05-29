namespace MathHunt.Contracts.Identity;

public record LoginCustomRequest(
    string userName,
    string password,
    bool rememberMe
    );