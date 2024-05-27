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
    public async Task<List<Core.Models.RoleModel>> Get()
    {
        var roleList = await roleManager.Roles
            .Select(r => new RoleModel {Id = Guid.Parse(r.Id), NameRole = r.Name }).ToListAsync();
        return roleList;
    }

    public async Task<List<string>> GetUser(string emailId)
    {
        var user = await userManager.FindByEmailAsync(emailId);
        var userRole = await userManager.GetRolesAsync(user);
        return userRole.ToList();
    }

    public async Task<List<string>> Add(string[] roles)
    {
        List<string> roleList = new List<string>();
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
                roleList.Add(role);
            }
        }

        return roleList;
    }

    public async Task<bool> AddUser(string emailId, string[] roles)
    {
        var user = await userManager.FindByEmailAsync(emailId);
        var existRole = await ExistingRole(roles);
        if (user != null && existRole.Count == roles.Length)
        {
            var assignRole = await userManager.AddToRolesAsync(user, existRole);
            return assignRole.Succeeded;
        }

        return false;
    }

    
    private async Task<List<string>> ExistingRole(string[] roles)
    {
        var roleList = new List<string>();
        foreach (var role in roles)
        {
            var roleExist = await roleManager.RoleExistsAsync(role);
            if (roleExist)
            {
                roleList.Add(role);
            }
        }

        return roleList;

    }
}