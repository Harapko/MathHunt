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
    public async Task<List<UserSkill>> GetUserSkills(string userName)
    {
        var userEntity = await context.Users
            .AsNoTracking()
            .Where(u => u.UserName == userName)
            .Include(u => u.UserSkillsEntities)
            .ThenInclude(us => us.SkillEntity)
            .FirstOrDefaultAsync();

        var userSkill = userEntity.UserSkillsEntities
            .Select(us => UserSkill
                .Create(us.AppUserId, us.SkillId, us.ProficiencyLevel, null, Skill
                    .Create(us.SkillId, us.SkillEntity.SkillName, []).userSkill).userSkill)
            .ToList();
            
    
        return userSkill;
    }

    public async Task<string> AddToUser(string userName, string skillName, string proficiencyLevel)
    {
        var skillEntity = await context.Skill
            .AsNoTracking()
            .Where(s => s.SkillName == skillName)
            .FirstOrDefaultAsync();
        var user = await userManager.FindByNameAsync(userName);
        if (user != null && skillEntity != null)
        {
            var userSkill = new UserSkillEntity
            {
                AppUserId = user.Id,
                SkillId = skillEntity.Id,
                ProficiencyLevel = proficiencyLevel
            };
            await context.UserSkill.AddAsync(userSkill);
        }

        await context.SaveChangesAsync();


        return user.UserName;
    }

    public async Task<string> UpdateSkill(string userId, Guid oldSkillId, Guid newSkillId, string proficiencyLevel)
    {
            await context.UserSkill
                .Where(us => us.AppUserId == userId)
                .Where(us => us.SkillId == oldSkillId)
                .ExecuteUpdateAsync(set => set
                    .SetProperty(us=>us.SkillId, newSkillId)
                    .SetProperty(us => us.ProficiencyLevel, proficiencyLevel));
            return userId;
    }

    public async Task<string> DeleteSkill(string userId, string skillName)
    {
        await context.UserSkill
            .Where(us => us.AppUserId == userId)
            .Where(us => us.SkillEntity.SkillName == skillName)
            .ExecuteDeleteAsync();
        return (userId);
    }
    
}