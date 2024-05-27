namespace MathHunt.Contracts.Identity;

public record AddUserRoleRequest(
    string email,
    string[] roles
    );