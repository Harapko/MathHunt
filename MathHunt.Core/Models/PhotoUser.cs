namespace MathHunt.Core.Models;

public class PhotoUser
{
    private PhotoUser()
    {
        
    }

    private PhotoUser(Guid id,string path, string appUserId)
    {
        Id = id;
        Path = path;
        AppUserId = appUserId;
    }

    public Guid Id { get; }

    public string Path { get; } = string.Empty;

    public string AppUserId { get; } 
    public AppUser AppUser { get; }

    public static (PhotoUser photoUser, string Error) Create(Guid id, string path, string appUserId)
    {
        var error = string.Empty;

        if (string.IsNullOrWhiteSpace(path))
        {
            error = "File has invalid path";
        }

        var photoUser = new PhotoUser(id, path, appUserId);

        return (photoUser, error);
    }
}