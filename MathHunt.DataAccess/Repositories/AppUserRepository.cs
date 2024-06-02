using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MathHunt.Core.Abstraction.IServices;
using MathHunt.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MathHunt.DataAccess.Repositories;

public class AppUserRepository(
    UserManager<AppUserEntity> userManager,
    SignInManager<AppUserEntity> signInManager,
    IRoleUserService roleService,
    AppDbContext context,
    IConfiguration configuration
) : IAppUserRepository
{
    public async Task<List<AppUserEntity>> Get()
    {
        var userEntity = await userManager.Users
            .AsNoTracking()
            .Include(u => u.UserSkillsEntities)
            .ToListAsync();

        foreach (var user in userEntity)
        {
            user.Role = await roleService.GetUserRole(user.Email);
        }

        return userEntity;
    }

    public async Task<AppUserEntity?> GetByName(string name)
    {
        var user = await userManager.Users
            .Where(u => u.UserName == name)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        user.Role = await roleService.GetUserRole(user.Email);
        return user;
    }
    
    public async Task<string> Register(AppUserEntity user, string password, string role)
    {
        var newUser = new AppUserEntity()
        {
            Id = user.Id,
            UserName = user.UserName,
            UserSurname = user.UserSurname,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
        };

        var createResult = await userManager.CreateAsync(newUser, password);
        if (createResult.Succeeded)
        {
            await roleService.AddRoleToUser(newUser.Email, role);
            await signInManager.SignInAsync(newUser, false);
        }
        else
        {
            var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
            throw new Exception($"Failed to create user: {errors}");
        }

        return newUser.Id;
    }
    

    public Task Logout()
    {
        var result = signInManager.SignOutAsync();
        if (result.IsCompleted)
        {
            
            return result;
        }
        else
        {
            throw new Exception($"Failed to create user: {result.Exception}");
        }
    }

    public async Task<bool> Delete(string email)
    {
        var user = await userManager.FindByIdAsync(email);
        var result = await userManager.DeleteAsync(user);
        return result.Succeeded;
    }
    
    public async Task<List<AppUserEntity>> GetSkillsUser(string userName)
    {
        var user = await context.Users
            .Where(u => u.UserName == userName)
            .Include(u => u.UserSkillsEntities)
            .ToListAsync();

        return user;
    }
    
    

}



