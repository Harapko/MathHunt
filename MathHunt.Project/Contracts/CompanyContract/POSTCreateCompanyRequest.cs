namespace MathHunt.Contracts.CompanyContract;

public record POSTCreateCompanyRequest(
    string tradeName,
    DateOnly dataStart,
    DateOnly dataEnd,
    string positionUser,
    string DescriptionUsersWork,
    string appUserId
);
