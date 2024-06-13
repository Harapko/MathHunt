using MathHunt.Core.Abstraction.IRepositories;
using MathHunt.Core.Models;
using MathHunt.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MathHunt.DataAccess.Repositories;

public class SkillRepository(AppDbContext context) : ISkillRepository
{
    public async Task<List<Skill>> Get()
    {
        var skillEntity = await context.Skill
            .Include(s => s.UserSkillEntities)
            .ToListAsync();

        var skill = skillEntity
            .Select(s => Skill.Create(s.Id, s.SkillName, []).userSkill)
            .ToList();

        return skill;
    }

    public async Task<List<Skill>> GetUsersBySkillName(string skillName)
    {
        var skillListEntity = await context.Skill
            .AsNoTracking()
            .Where(s => s.SkillName == skillName)
            .Include(u => u.UserSkillEntities)
            .ToListAsync();
        var skill = skillListEntity
            .Select(s => Skill
                .Create(s.Id, s.SkillName, s.UserSkillEntities
                    .Select(us => UserSkill
                        .Create(us.AppUserId, us.SkillId, us.ProficiencyLevel, null, null)
                        .userSkill)
                    .ToList())
                .userSkill)
            .ToList();

        return skill;
    }

    public async Task<Guid> Create(Skill skill)
    {
        var userSkillEntity = new SkillEntity()
        {
            Id = skill.Id,
            SkillName = skill.SkillName,
        };

        await context.Skill.AddAsync(userSkillEntity);
        await context.SaveChangesAsync();
        return userSkillEntity.Id;
    }

    public async Task<Guid> Update(Guid id, string skillName)
    {
        await context.Skill
            .Where(s => s.Id == id)
            .ExecuteUpdateAsync(set => set
                .SetProperty(s => s.SkillName, skillName));

        return id;
    }

    public async Task<Guid> Delete(Guid id)
    {
        await context.Skill
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync();
        return id;
    }

    
}