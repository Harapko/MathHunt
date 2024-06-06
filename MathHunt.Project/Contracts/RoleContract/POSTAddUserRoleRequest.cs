namespace MathHunt.Contracts.Identity;

public record POSTAddUserRoleRequest(
    string email,
    string roles
    );