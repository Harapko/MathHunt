namespace MathHunt.Core.Models;

public class Company
{
    private Company()
    {
        
    }

    private Company(Guid id, string tradeName, DateOnly? dataStart, DateOnly? dataEnd, string? positionUser, string? descriptionUsersWork, string? link, string appUserId, List<CompanySkill> companySkills)
    {
        Id = id;
        TradeName = tradeName;
        DataStart = dataStart;
        DataEnd = dataEnd;
        PositionUser = positionUser;
        DescriptionUsersWork = descriptionUsersWork;
        Link = link;
        AppUserId = appUserId;
        CompanySkills = companySkills;
    }

    public Guid Id { get; }
    public string TradeName { get; } = string.Empty;
    public DateOnly? DataStart { get; } 
    public DateOnly? DataEnd { get; }
    public string? PositionUser { get; } = string.Empty;
    public string? DescriptionUsersWork { get; } = string.Empty;
    public string? Link { get; } = string.Empty;
    public string AppUserId { get; }
    public AppUser AppUser { get; }
    public List<CompanySkill> CompanySkills { get; }

    public static (Company company, string Error) Create(Guid id, string tradeName, DateOnly? dataStart,
        DateOnly? dataEnd, string? positionUser, string? descriptionUsersWork, string? link, string appUserId, List<CompanySkill> companySkills)
    {
        var error = string.Empty;

        if (string.IsNullOrWhiteSpace(tradeName))
        {
            error = "Company must have a name";
        }

        if (string.IsNullOrWhiteSpace(positionUser))
        {
            error = "Position user can`t be null";
        }

        if (string.IsNullOrWhiteSpace(appUserId))
        {
            error = "Company must have a User";
        }

        var company = new Company(id, tradeName, dataStart, dataEnd, positionUser, descriptionUsersWork, link, appUserId, companySkills);
        return (company, error);
        
    }
}