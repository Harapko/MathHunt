using MathHunt.Core.Abstraction.IRepositories;
using MathHunt.Core.Models;
using MathHunt.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MathHunt.DataAccess.Repositories;

public class UserSkillRepository(
    AppDbContext context,
    UserManager<AppUserEntity> userManager
    ) : IUserSkillRepository
{
    public async Task<List<UserSkill>> Get()
    {
        var skillListEntity = await context.UserSkill
            .AsNoTracking()
            .Include(s => s.AppUserEntities)
            .ToListAsync();
        var skill = skillListEntity
            .Select(s => UserSkill.Create(s.Id, s.SkillName).userSkill)
            .ToList();

        return skill;
    }
    
    public async Task<List<UserSkill>> GetByName(string skillName)
    {
        var skillListEntity = await context.UserSkill
            .AsNoTracking()
            .Where(s=>s.SkillName == skillName)
            .ToListAsync();
        var skill = skillListEntity
            .Select(s => UserSkill.Create(s.Id, s.SkillName).userSkill)
            .ToList();

        return skill;
    }
    
    public async Task<string> AddToUser(string emailId ,string skillName)
    {
        var skillEntity = await context.UserSkill
            .AsNoTracking()
            .Where(s => s.SkillName == skillName)
            .FirstOrDefaultAsync();
        var user = await userManager.FindByEmailAsync(emailId);
        if (user != null && skillEntity != null)
        {
            user.UserSkillsEntities?.Add(skillEntity);
        }

        await context.SaveChangesAsync();
        

        return user.Email;
    }

    public async Task<Guid> Create(UserSkill userSkill)
    {
        var userSkillEntity = new UserSkillEntity()
        {
            Id = userSkill.Id,
            SkillName = userSkill.SkillName,
        };

        await context.UserSkill.AddAsync(userSkillEntity);
        await context.SaveChangesAsync();
        return userSkillEntity.Id;
    }

    public async Task<Guid> Update(Guid id, string skillName)
    {
        await context.UserSkill
            .Where(s => s.Id == id)
            .ExecuteUpdateAsync(set => set
                .SetProperty(s => s.SkillName, skillName));

        return id;
    }

    public async Task<Guid> Delete(Guid id)
    {
        await context.UserSkill
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync();
        return id;
    }
    

    
    
}