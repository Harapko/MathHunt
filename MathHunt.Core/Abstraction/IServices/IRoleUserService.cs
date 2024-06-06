using MathHunt.Core.Models;

namespace MathHunt.Core.Abstraction.IServices;

public interface IRoleUserService
{ 
    Task<List<RoleModel>> GetRoles();
    Task<string> GetUserRole(string userName);
    Task<string> AddRole(string roles);
    Task<bool> AddRoleToUser(string emailId, string roles);
    Task<string> DeleteRole(string role);
}