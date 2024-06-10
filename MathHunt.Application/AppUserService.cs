using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;
using MathHunt.DataAccess;

namespace MathHunt.Application;

public class AppUserService(IAppUserRepository repository) : IAppUserService
{
    public async Task<List<AppUser>> GetAllUser()
    {
        return await repository.Get();
    }

    public async Task<AppUser?> GetUserByName(string name)
    {
        return await repository.GetByName(name);
    }
    
    public async Task<string> RegisterUser(AppUser user, string password, string role)
    {
        return await repository.Register(user, password, role);
    }

    public async Task<string> UpdateUser(string userName, AppUser user)
    {
        return await repository.Update(userName, user);
    }
    
    public async Task<bool> DeleteUser(string userName)
    {
        return await repository.Delete(userName);
    }

    public async Task<string> BanUser(string userName)
    {
        return await repository.Ban(userName);
    }
    
    
}