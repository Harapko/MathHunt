using MathHunt.Core.Models;

namespace MathHunt.DataAccess.Entities;

public class CompanyEntity
{
    public Guid Id { get; set; }
    public string TradeName { get; set; } = string.Empty;
    public DateOnly? DataStart { get; set; }
    public DateOnly? DataEnd { get; set; }
    public string? PositionUser { get; set; } = string.Empty;
    public string? DescriptionUsersWork { get; set; } = string.Empty;
    public string AppUserId { get; set; }
    public AppUserEntity AppUser { get; set; }
}