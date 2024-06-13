namespace MathHunt.Contracts.CompanyContract;

public record POSTCreateCompanyRequest(
    string tradeName,
    DateOnly dataStart,
    DateOnly dataEnd,
    string positionUser,
    string descriptionUsersWork,
    string link,
    string appUserId
);
