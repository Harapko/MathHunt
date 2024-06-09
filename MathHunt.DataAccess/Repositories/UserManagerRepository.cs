using MathHunt.Core.Abstraction.IRepositories;
using MathHunt.Core.Models;
using MathHunt.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MathHunt.DataAccess.Repositories;

public class UserManagerRepository(
    AppDbContext context,
    UserManager<AppUserEntity> userManager) : IUserManagerRepository
{
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

    public async Task<string> AddToUser(string userName, string skillName)
    {
        var skillEntity = await context.UserSkill
            .AsNoTracking()
            .Where(s => s.SkillName == skillName)
            .FirstOrDefaultAsync();
        var user = await userManager.FindByNameAsync(userName);
        if (user != null && skillEntity != null)
        {
            user.UserSkillsEntities?.Add(skillEntity);
        }

        await context.SaveChangesAsync();


        return user.Email;
    }

    public async Task<bool> DeleteSkill(string userName, string skillName)
    {
        var skill = await context.UserSkill
            .Include(s=>s.AppUserEntities)
            .FirstOrDefaultAsync(s => s.SkillName == skillName);

        var user = await userManager.FindByNameAsync(userName);

        var resp = skill.AppUserEntities.Remove(user);
        await context.SaveChangesAsync();
        return resp;
    }
}