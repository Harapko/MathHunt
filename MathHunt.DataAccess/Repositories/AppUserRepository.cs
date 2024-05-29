using System.Threading.Channels;
using MathHunt.Core.Abstraction.IRepositories;
using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;
using MathHunt.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MathHunt.DataAccess.Repositories;

public class AppUserRepository(
    UserManager<AppUserEntity> userManager,
    SignInManager<AppUserEntity> signInManager,
    IRoleUserService roleService,
    AppDbContext context
    ) : IAppUserRepository
{
    public async Task<List<AppUserEntity>> Get()
    {
        var userEntity = await userManager.Users
            .AsNoTracking()
            .Include(u=>u.UserSkillsEntities)
            .ToListAsync();

        return userEntity;
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

    public async Task<bool> Login(string userName, string password, bool rememberMe)
    {
        var result = await signInManager.PasswordSignInAsync(userName, password, rememberMe, false);
        if (result.Succeeded)
        {
            return result.Succeeded;
        }
        else
        {
            throw new Exception($"Failed to create user: {result.IsNotAllowed}");
        }
    }
    
    public Task Logout()
    {
        var result =  signInManager.SignOutAsync();
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
        var user = await userManager.FindByEmailAsync(email);
        var result = await userManager.DeleteAsync(user);
        return result.Succeeded;
    }

    public async Task<AppUserEntity?> GetSkillsUser(string email)
    {
        var user = await userManager.Users
            .Where(u => u.Email == email)
            .Include(u => u.UserSkillsEntities)
            .FirstOrDefaultAsync();

        return user;
    }
}