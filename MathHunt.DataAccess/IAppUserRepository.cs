using MathHunt.DataAccess.Entities;

namespace MathHunt.DataAccess;

public interface IAppUserRepository
{
    Task<List<AppUserEntity>> Get();
    Task<AppUserEntity?> GetByName(string name);
    Task<string> Register(AppUserEntity user, string password, string role);
    Task<bool> Delete(string email);
    Task<List<AppUserEntity>> GetSkillsUser(string userName);
    Task Logout();
}