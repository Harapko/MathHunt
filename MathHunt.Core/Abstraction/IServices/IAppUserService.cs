using MathHunt.Core.Models;

namespace MathHunt.Core.Abstraction.IServices;

public interface IAppUserService
{
    Task<List<AppUser>> GetAllUser();
    Task<string> RegisterUser(AppUser user, string password, string role);
    Task<bool> LoginUser(string email, string password, bool rememberMe, bool lockOnFail);
    Task<bool> DeleteUser(string email);
    Task<AppUser?> GetUsersSkill(string email);
}