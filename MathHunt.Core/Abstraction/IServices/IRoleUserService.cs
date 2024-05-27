using MathHunt.Core.Models;

namespace MathHunt.Core.Abstraction.IServices;

public interface IRoleUserService
{ 
    Task<List<RoleModel>> GetRoles();
    Task<List<string>> GetUserRole(string emailId);
    Task<List<string>> AddRole(string[] roles);
    Task<bool> AddRoleToUser(string emailId, string[] roles);
}