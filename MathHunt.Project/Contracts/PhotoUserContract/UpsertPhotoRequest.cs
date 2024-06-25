namespace MathHunt.Contracts.Role;

public record UpsertPhotoRequest(
    string appUserId,
    IFormFile path
    );