namespace MathHunt.Contracts.CompanyContract;

public record PUTUpdateCompanyRequest(
    string tradeName,
    DateOnly dataStart,
    DateOnly dataEnd,
    string positionUser,
    string descriptionUsersWork,
    string link,
    string appUserId
    );