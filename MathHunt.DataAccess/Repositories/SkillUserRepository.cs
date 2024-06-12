using MathHunt.Core.Abstraction.IRepositories;
using MathHunt.Core.Models;
using MathHunt.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MathHunt.DataAccess.Repositories;

public class SkillUserRepository(AppDbContext context) : ISkillUserRepository
{
    public async Task<List<UserSkill>> Get()
    {
        var skillEntity = await context.UserSkill
            .Include(s => s.AppUserEntities)
            .ToListAsync();

        var skill = skillEntity
            .Select(s => UserSkill.Create(s.Id, s.SkillName, []).userSkill)
            .ToList();

        return skill;
    }

    public async Task<List<UserSkill>> GetUsersBySkillName(string skillName)
    {
        var skillListEntity = await context.UserSkill
            .AsNoTracking()
            .Where(s => s.SkillName == skillName)
            .Include(u => u.AppUserEntities)
            .ToListAsync();
        var skill = skillListEntity
            .Select(s => UserSkill.Create(s.Id, s.SkillName, s.AppUserEntities
                .Select(u=> AppUser.Create(u.Id, u.UserName, u.UserSurname, u.Email, u.PhoneNumber,
                    u.EnglishLevel, u.DescriptionSkill, u.GitHubLink, u.Role, [], [], []).appUser).ToList()).userSkill)
            .ToList();

        return skill;
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