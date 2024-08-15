using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;
using MathHunt.DataAccess;
using MathHunt.DataAccess.Skill.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;


public class GetAllSkillHandler(AppDbContext context) : IRequestHandler<GetAllSkillQuery, List<Skill>>
{
    public async Task<List<Skill>> Handle(GetAllSkillQuery request, CancellationToken cancellationToken)
    {
        var skillEntity = await context.Skill
            .AsNoTracking()
            .Include(s => s.UserSkillEntities)
            .ToListAsync(cancellationToken: cancellationToken);
        
        var skill = skillEntity
            .Select(s => Skill.Create(s.Id, s.SkillName, []).userSkill)
            .ToList();

        return skill;
    }
}