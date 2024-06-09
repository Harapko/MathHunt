namespace MathHunt.Contracts.CompanyContract;

public record PUTUpdateCompanyRequest(
        string tradeName,
    DateOnly dataStart,
    DateOnly dataEnd,
    string positionUser,
    string DescriptionUsersWork,
    string appUserId
    );