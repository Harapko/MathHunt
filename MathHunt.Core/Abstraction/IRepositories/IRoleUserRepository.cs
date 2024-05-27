using MathHunt.Core.Models;

namespace MathHunt.Core.Abstraction.IRepositories;

public interface IRoleUserRepository
{
    Task<List<RoleModel>> Get();
    Task<List<string>> GetUser(string emailId);
    Task<List<string>> Add(string[] roles);
    Task<bool> AddUser(string emailId, string[] roles);
    
}

