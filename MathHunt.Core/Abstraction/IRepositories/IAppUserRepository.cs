using MathHunt.Core.Models;

namespace MathHunt.DataAccess;

public interface IAppUserRepository
{
    Task<List<AppUser>> Get();
    Task<AppUser?> GetById(string id);
    Task<string> Register(AppUser user, string password, string role);
    Task<string> Update(string userId, AppUser user);
    Task<bool> Delete(string userName);
    Task<string> Ban(string userName);
    
    // Task Logout();
}