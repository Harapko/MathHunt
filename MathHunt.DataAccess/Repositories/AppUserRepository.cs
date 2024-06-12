using MathHunt.Core.Abstraction.IRepositories;
using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;
using MathHunt.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MathHunt.DataAccess.Repositories;

public class AppUserRepository(
    UserManager<AppUserEntity> userManager,
    IRoleUserService roleService
) : IAppUserRepository
{
    public async Task<List<AppUser>> Get()
    {
        var userEntity = await userManager.Users
            .AsNoTracking()
            .Include(u => u.UserSkillsEntities)
            .Include(u=>u.PhotoUserEntities)
            .ToListAsync();

        foreach (var user in userEntity)
        {
            user.Role = await roleService.GetUserRole(user.UserName);
        }

        var userList = userEntity
            .Select(u => AppUser.Create(u.Id ,u.UserName, u.UserSurname, u.Email, u.PhoneNumber, u.EnglishLevel, u.DescriptionSkill, u.GitHubLink, u.Role, u.UserSkillsEntities
                .Select(s=> UserSkill.Create(s.Id, s.SkillName, []).userSkill)
                .ToList(), u.CompaniesEntity
                .Select(c=> Company.Create(c.Id, c.TradeName, c.DataStart, c.DataEnd, c.PositionUser, c.DescriptionUsersWork, c.AppUserId).company)
                .ToList(), u.PhotoUserEntities
                .Select(p=>PhotoUser.Create(p.Id, p.Path, p.AppUserEntityId).photoUser)
                .ToList()).appUser)
            .ToList();

        return userList;
    }

    public async Task<AppUser> GetById(string id)
    {
        var userEntity = await userManager.Users
            .AsNoTracking()
            .Where(u => u.Id == id)
            .Include(u=>u.UserSkillsEntities)
            .ToListAsync();
        
        foreach (var users in userEntity)
        {
            users.Role = await roleService.GetUserRole(users.UserName);
        }

        var user = userEntity
            .Select(u => AppUser.Create(u.Id ,u.UserName, u.UserSurname, u.Email, u.PhoneNumber, u.EnglishLevel, u.DescriptionSkill, u.GitHubLink, u.Role, u.UserSkillsEntities
                .Select(s=> UserSkill.Create(s.Id, s.SkillName, []).userSkill)
                .ToList(), u.CompaniesEntity
                .Select(c=> Company.Create(c.Id, c.TradeName, c.DataStart, c.DataEnd, c.PositionUser, c.DescriptionUsersWork, c.AppUserId).company)
                .ToList(), []).appUser)
            .FirstOrDefault();
        return user;
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
            DescriptionSkill = user.DescriptionSkill,
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

    public async Task<string> Update(string userName, AppUser user)
    {
        await userManager.Users
            .Where(u => u.UserName == userName)
            .ExecuteUpdateAsync(set => set
                .SetProperty(u => u.UserName, user.UserName)
                .SetProperty(u => u.UserSurname, user.UserSurname)
                .SetProperty(u => u.Email, user.Email)
                .SetProperty(u => u.PhoneNumber, user.PhoneNumber)
                .SetProperty(u => u.EnglishLevel, user.EnglishLevel)
                .SetProperty(u => u.DescriptionSkill, user.DescriptionSkill));
        
        return user.Id;
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

    public async Task<string> Ban(string userName)
    {
        var user = await userManager.FindByNameAsync(userName);
        if (user.LockoutEnd <= DateTimeOffset.Now)
        {
            await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Now.AddDays(10).ToOffset(default));
            return $"User {user.UserName} is block";
        }
        else
        {
            await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Now.ToOffset(default));
            return $"User {user.UserName} is unblock";
        }
        
    }
    
    
    

}



