using MathHunt.DataAccess.Entities;

namespace MathHunt.DataAccess;

public interface IAppUserRepository
{
    Task<List<AppUserEntity>> Get();
    Task<string> Register(AppUserEntity user, string password, string role);
    Task<string> Login(string email, string password, bool rememberMe);
    Task<bool> Delete(string email);
    Task<List<AppUserEntity>> GetSkillsUser(string userName);
    Task Logout();
}