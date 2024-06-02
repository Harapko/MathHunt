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

    public async Task<string> Login(string email, string password, bool rememberMe)
    {
        var result = await signInManager.PasswordSignInAsync(email, password, rememberMe, false);
        if (result.Succeeded)
        {
            var user = await userManager.FindByEmailAsync(email);
            var token = GenerateJwtToken(user);
            return await token;
        }
        else
        {
            throw new Exception($"Failed to create user: {result.IsNotAllowed}");
        }
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
        var user = await userManager.FindByEmailAsync(email);
        var result = await userManager.DeleteAsync(user);
        return result.Succeeded;
    }

    // public async Task<AppUserEntity?> GetSkillsUser(string userName)
    // {
    //     var user = await userManager.Users
    //         .Where(u => u.UserName == userName)
    //         .Include(u => u.UserSkillsEntities)
    //         .FirstOrDefaultAsync();
    //
    //     return user;
    // }
    
    public async Task<List<AppUserEntity>> GetSkillsUser(string userName)
    {
        var user = await context.Users
            .Where(u => u.UserName == userName)
            .Include(u => u.UserSkillsEntities)
            .ToListAsync();

        return user;
    }
    
    private async Task<string> GenerateJwtToken(AppUserEntity user)
    {
        // Получите роли пользователя
        var roles = await userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Добавьте роли в утверждения
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}



