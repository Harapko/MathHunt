using System.Formats.Asn1;
using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;
using MathHunt.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MathHunt.DataAccess.Repositories;

public class AppUserRepository(
    UserManager<AppUserEntity> userManager,
    IRoleUserService roleService,
    ICacheService cacheService,
    ILogger<AppUserRepository> logger,
    IConfiguration configuration
    ) : IAppUserRepository
{
    public async Task<List<AppUser>> Get()
    { 
        List<AppUserEntity> userListEntity;
        
        var cacheData = await cacheService
            .GetData<List<AppUserEntity>>(configuration["AppUserRedis"] ?? string.Empty);
        if (cacheData is null)
        { 
            userListEntity = await userManager.Users
                .AsNoTracking()
                .Include(u=>u.PhotoUserEntities)
                .Include(u=>u.UserSkillsEntities)
                .ToListAsync();
                
            await cacheService.SetData(configuration["AppUserRedis"] ?? string.Empty, userListEntity);
            logger.LogInformation("Data from Db");
        }
        else
        {
            logger.LogInformation("Data from cache");
            userListEntity = cacheData;
        }
        

        foreach (var user in userListEntity)
        {
            user.Role = await roleService.GetUserRole(user.UserName);
        }

        var userList = userListEntity
            .Select(u => AppUser.Create(u.Id, u.UserName, u.UserSurname, u.Email, u.PhoneNumber, u.EnglishLevel,
                u.DescriptionSkill, u.GitHubLink, u.Role, u.LockoutEnd.Value.Date, u.IsLock, [], [], u.PhotoUserEntities
                    .Select(p=> PhotoUser
                        .Create(p.Id, p.Path, p.AppUserEntityId).photoUser)
                    .ToList()).appUser)
            .ToList();

        return userList;    
    }

    public async Task<AppUser?> GetById(string id)
    {
        var userList = await Get();
        var user = userList.FirstOrDefault(u => u.Id == id);
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
            await cacheService.AddItemToCollection<AppUserEntity>(configuration["AppUserRedis"] ?? string.Empty, await userManager.FindByIdAsync(newUser.Id));
        }
        else
        {
            var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
            throw new Exception($"Failed to create user: {errors}");
        }
    
        return newUser.Id;
    }

    public async Task<string> Update(string userId, AppUser user)
    {
        await userManager.Users
            .Where(u => u.Id == userId)
            .ExecuteUpdateAsync(set => set
                // .SetProperty(u => u.UserName, user.UserName)
                .SetProperty(u => u.UserSurname, user.UserSurname)
                .SetProperty(u => u.Email, user.Email)
                .SetProperty(u => u.PhoneNumber, user.PhoneNumber)
                .SetProperty(u => u.EnglishLevel, user.EnglishLevel)
                .SetProperty(u => u.GitHubLink, user.GitHubLink)
                .SetProperty(u => u.DescriptionSkill, user.DescriptionSkill));

        var userEntity = await userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (userEntity is not null)
        {
            await cacheService.UpdateItemToCollection<AppUserEntity>(configuration["AppUserRedis"] ?? string.Empty, x=> x.Id == userId, userEntity);
        }
        
        return user.Id;
    }
    
    
    public async Task<bool> Delete(string userName)
    {
        var user = await userManager.FindByNameAsync(userName);
        var result = await userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            await cacheService.DeleteItemToCollection<AppUserEntity>(configuration["AppUserRedis"] ?? string.Empty, x => x.UserName == userName);
            return true;
        }
        return false;
    }

    public async Task<string> Ban(string userName)
    {
        var user = await userManager.FindByNameAsync(userName);
        user.IsLock = !user.IsLock;
        await userManager.UpdateAsync(user);
        if (user.LockoutEnd <= DateTimeOffset.Now.AddDays(1).ToOffset(default)) 
        {
            await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Now.AddDays(10).ToOffset(default));
            await cacheService.UpdateItemToCollection<AppUserEntity>(configuration["AppUserRedis"] ?? string.Empty, x=> x.UserName == userName, user);
            return $"User {user.UserName} is block";
        }

        await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Now.ToOffset(default));
        await cacheService.UpdateItemToCollection<AppUserEntity>(configuration["AppUserRedis"] ?? string.Empty, x=> x.UserName == userName, user);
        return $"User {user.UserName} is unblock";

    }
    
}



