namespace MathHunt.DataAccess.Entities;

public class PhotoUserEntity
{
    public Guid Id { get; set; }

    public string Path { get; set; } = string.Empty;

    public string AppUserEntityId { get; set; } 
    public AppUserEntity AppUserEntity { get; set; }
}