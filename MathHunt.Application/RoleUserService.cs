using MathHunt.Core.Abstraction.IRepositories;
using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;

namespace MathHunt.Application;

public class RoleUserService(IRoleUserRepository userRepository) : IRoleUserService
{
    public async Task<List<RoleModel>> GetRoles()
    {
        return await userRepository.Get();
    }

    public async Task<string> GetUserRole(string userName)
    {
        return await userRepository.GetUser(userName);
    }

    public async Task<string> AddRole(string roles)
    {
        return await userRepository.Add(roles);
    }

    public async Task<bool> AddRoleToUser(string emailId, string roles)
    {
        return await userRepository.AddUser(emailId, roles);
    }

    public async Task<string> DeleteRole(string role)
    {
       return await userRepository.Delete(role);
    }
}