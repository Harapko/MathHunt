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
        var roleList = await roleManager.Roles
            .Select(r => new RoleModel { Id = Guid.Parse(r.Id), NameRole = r.Name }).ToListAsync();
        return roleList;
    }

    public async Task<string> GetUser(string emailId)
    {
        var user = await userManager.FindByEmailAsync(emailId);
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


    // private async Task<List<string>> ExistingRole(string[] roles)
    // {
    //     var roleList = new List<string>();
    //     foreach (var role in roles)
    //     {
    //         var roleExist = await roleManager.RoleExistsAsync(role);
    //         if (roleExist)
    //         {
    //             roleList.Add(role);
    //         }
    //     }
    //
    //     return roleList;
    //
    // }
}