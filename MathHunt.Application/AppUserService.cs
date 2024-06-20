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

    public async Task<AppUser?> GetUserById(string id)
    {
        return await repository.GetById(id);
    }
    
    public async Task<string> RegisterUser(AppUser user, string password, string role)
    {
        return await repository.Register(user, password, role);
    }

    public async Task<string> UpdateUser(string userId, AppUser user)
    {
        return await repository.Update(userId, user);
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