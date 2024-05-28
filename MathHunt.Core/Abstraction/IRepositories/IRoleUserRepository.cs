using MathHunt.Core.Models;

namespace MathHunt.Core.Abstraction.IRepositories;

public interface IRoleUserRepository
{
    Task<List<RoleModel>> Get();
    Task<string> GetUser(string emailId);
    Task<string> Add(string roles);
    Task<bool> AddUser(string emailId, string roles);
    Task<string> Delete(string role);

}

