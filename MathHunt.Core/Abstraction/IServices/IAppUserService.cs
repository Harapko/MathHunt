using MathHunt.Core.Models;

namespace MathHunt.Core.Abstraction.IServices;

public interface IAppUserService
{
    Task<List<AppUser>> GetAllUser();
    Task<AppUser?> GetUserById(string name);
    Task<string> RegisterUser(AppUser user, string password, string role);
    Task<string> UpdateUser(string userName, AppUser user);
    Task<bool> DeleteUser(string userName);
    Task<string> BanUser(string userName);

}