using Microsoft.AspNetCore.Identity;

namespace MathHunt.Core.Models;

public class RoleModel : IdentityRole
{
    private RoleModel()
    {
        
    }

    private RoleModel(string nameRole)
    {
        NameRole = nameRole;
    }

    public string NameRole { get; } = string.Empty;

    
    public static (RoleModel roleModel, string Error) Create(string nameRole)
    {
        var error = string.Empty;
        if (string.IsNullOrWhiteSpace(nameRole))
        {
            error = "Role can`t be null";
        }

        
        var roleModel = new RoleModel(nameRole);

        return (roleModel, error);
    }
}