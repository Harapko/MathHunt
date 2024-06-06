using MathHunt.Core.Models;

namespace MathHunt.DataAccess;

public interface IAppUserRepository
{
    Task<List<AppUser>> Get();
    Task<AppUser?> GetByName(string name);
    Task<string> Register(AppUser user, string password, string role);
    Task<bool> Delete(string userName);
    Task<List<string>> GetUserSkills(string userName);
    // Task Logout();
}