using MathHunt.Core.Models;

namespace MathHunt.Core.Abstraction.IServices;

public interface IAppUserService
{
    Task<List<AppUser>> GetAllUser();
    Task<AppUser?> GetUserByName(string name);
    Task<string> RegisterUser(AppUser user, string password, string role);
    Task<bool> DeleteUser(string userName);
    Task<List<string>> GetUsersSkill(string userName);
}