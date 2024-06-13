namespace MathHunt.Contracts.CompanyContract;

public record GETCompanyByUserResponse(
    Guid Id,
    string TradeName,
    DateOnly? DataStart,
    DateOnly? DataEnd,
    string? PositionUser,
    string? DescriptionUsersWork,
    string? Link,
    string AppUserId,
    string[] skillName
    );