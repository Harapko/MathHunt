using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;
using MathHunt.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MathHunt.DataAccess.Repositories;

public class AppUserRepository(
    UserManager<AppUserEntity> userManager,
    IRoleUserService roleService,
    AppDbContext context
) : IAppUserRepository
{
    public async Task<List<AppUser>> Get()
    {
        var userEntity = await userManager.Users
            .AsNoTracking()
            .Include(u => u.UserSkillsEntities)
            .ToListAsync();

        foreach (var user in userEntity)
        {
            user.Role = await roleService.GetUserRole(user.UserName);
        }

        var userList = userEntity
            .Select(u => AppUser.Create(u.UserName, u.UserSurname, u.Email, u.PhoneNumber, u.EnglishLevel, u.Role, u.UserSkillsEntities
                .Select(s=> UserSkill.Create(s.Id, s.SkillName).userSkill).ToList()).appUser)
            .ToList();

        return userList;
    }

    public async Task<AppUser> GetByName(string name)
    {
        var userEntity = await userManager.Users
            .AsNoTracking()
            .Where(u => u.UserName == name)
            .Include(u=>u.UserSkillsEntities)
            .ToListAsync();
        
        foreach (var users in userEntity)
        {
            users.Role = await roleService.GetUserRole(users.UserName);
        }

        var user = userEntity
            .Select(u => AppUser.Create(u.UserName, u.UserSurname, u.Email, u.PhoneNumber, u.EnglishLevel, u.Role, u.UserSkillsEntities
                .Select(s => UserSkill.Create(s.Id, s.SkillName).userSkill).ToList()).appUser)
            .FirstOrDefault();
        return user;
    }
    
    public async Task<List<string>> GetUserSkills(string userName)
    {
        var userEntity = await context.Users
            .AsNoTracking()
            .Where(u => u.UserName == userName)
            .Include(u => u.UserSkillsEntities)
            .FirstOrDefaultAsync();

        var userSkill = userEntity.UserSkillsEntities
            .Select(s => s.SkillName)
            .ToList();
    
        return userSkill;
    }
    
    public async Task<string> Register(AppUser user, string password, string role)
    {
        var newUser = new AppUserEntity()
        {
            Id = user.Id,
            UserName = user.UserName,
            UserSurname = user.UserSurname,
            PhoneNumber = user.PhoneNumber,
            EnglishLevel = user.EnglishLevel,
            Email = user.Email,
        };
    
        var createResult = await userManager.CreateAsync(newUser, password);
        if (createResult.Succeeded)
        {
            await roleService.AddRoleToUser(newUser.Email, role);
        }
        else
        {
            var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
            throw new Exception($"Failed to create user: {errors}");
        }
    
        return newUser.Id;
    }
    
    // public Task Logout()
    // {
    //     var result = signInManager.SignOutAsync();
    //     if (result.IsCompleted)
    //     {
    //         
    //         return result;
    //     }
    //     else
    //     {
    //         throw new Exception($"Failed to create user: {result.Exception}");
    //     }
    // }
    
    public async Task<bool> Delete(string userName)
    {
        var user = await userManager.FindByNameAsync(userName);
        var result = await userManager.DeleteAsync(user);
        return result.Succeeded;
    }
    
    
    
    

}



