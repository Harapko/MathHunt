namespace MathHunt.Core.Models;

public sealed class AppUser
{
    private AppUser()
    {
    }

    private AppUser(string id, string userName, string? userSurname, string email, string? phoneNumber,
        string? englishLevel, string? descriptionSkill, string gitHubLink, string role, DateTime? lockEnd, bool isLock, List<UserSkill>? userSkills,
        List<Company> companies, List<PhotoUser> photoUsers)
    {
        Id = id;
        UserName = userName;
        UserSurname = userSurname;
        Email = email;
        PhoneNumber = phoneNumber;
        EnglishLevel = englishLevel;
        DescriptionSkill = descriptionSkill;
        GitHubLink = gitHubLink;
        Role = role;
        LockEnd = lockEnd;
        IsLock = isLock;
        UserSkills = userSkills;
        Companies = companies;
        PhotoUsers = photoUsers;
    }

    public string Id { get; }
    public string UserName { get; } = string.Empty;
    public string? UserSurname { get; } = string.Empty;
    public string Email { get; } = string.Empty;
    public string? PhoneNumber { get; } = string.Empty;
    public string? EnglishLevel { get; } = string.Empty;
    public string? DescriptionSkill { get; } = string.Empty;
    public string GitHubLink { get; } = string.Empty;
    public string Role { get; } = string.Empty;
    public DateTime? LockEnd { get; }
    public bool IsLock { get; } = true;
    public List<UserSkill>? UserSkills { get; } = [];
    public List<Company> Companies { get; } = [];
    public List<PhotoUser> PhotoUsers { get; } = [];

    public static (AppUser appUser, string Error) Create(string id, string userName, string? userSurname, string email,
        string phoneNumber,
        string? englishLevel, string? descriptionSkill, string gitHubLink, string role, DateTime? lockEnd, bool isLock, List<UserSkill>? userSkills,
        List<Company> companies, List<PhotoUser> photoUsers)
    {
        var error = string.Empty;
        if (string.IsNullOrWhiteSpace(userName))
        {
            error = "User must have a name";
        }

        if (!string.IsNullOrWhiteSpace(email))
        {
            error = "Email is empty";
        }

        if (string.IsNullOrWhiteSpace(role))
        {
            error = "Role is null";
        }

        var appUser = new AppUser(id, userName, userSurname, email, phoneNumber, englishLevel, descriptionSkill,
            gitHubLink, role, lockEnd, isLock, userSkills, companies, photoUsers);
        return (appUser, error);
    }
}