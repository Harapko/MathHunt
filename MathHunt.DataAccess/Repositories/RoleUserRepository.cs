using MathHunt.Core.Abstraction.IRepositories;
using MathHunt.Core.Models;
using MathHunt.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MathHunt.DataAccess.Repositories;

public class RoleUserRepository(
    RoleManager<IdentityRole> roleManager,
    UserManager<AppUserEntity> userManager)
    : IRoleUserRepository
{
    public async Task<List<RoleModel>> Get()
    {
        var roleEntity = await roleManager.Roles
            .AsNoTracking()
            .ToListAsync();

        var role = roleEntity
            .Select(r => RoleModel.Create(r.Name).roleModel)
            .ToList();
        return role;
        
    }

    public async Task<string> GetUser(string userName)
    {
        var user = await userManager.FindByNameAsync(userName);
        var userRole = await userManager.GetRolesAsync(user);
        var role = userRole.ToList().FirstOrDefault();
        return role;
    }

    public async Task<string> Add(string roles)
    {
        string role = string.Empty;

        if (!await roleManager.RoleExistsAsync(roles))
        {
            await roleManager.CreateAsync(new IdentityRole(roles));
            role = roles;
        }


        return role;
    }

    public async Task<bool> AddUser(string emailId, string roles)
    {
        var user = await userManager.FindByEmailAsync(emailId);
        var existRole = await roleManager.RoleExistsAsync(roles);
        if (user != null && existRole)
        {
            var assignRole = await userManager.AddToRoleAsync(user, roles);
            return assignRole.Succeeded;
        }

        return false;
    }

    public async Task<string> Delete(string role)
    {
        await roleManager.Roles
            .Where(r => r.Name == role)
            .ExecuteDeleteAsync();
        return role;
    }
    
}