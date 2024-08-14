using MathHunt.Core.Models;
using MathHunt.DataAccess.Entities;
using MathHunt.DataAccess.Skill.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MathHunt.DataAccess.Skill.Handlers;

public class GetUserBySkillNameHandler(AppDbContext context) : IRequestHandler<GetUserBySkillNameQueries, List<AppUserEntity>>
{
    public async Task<List<AppUserEntity>> Handle(GetUserBySkillNameQueries request, CancellationToken cancellationToken)
    {
        var skillListEntity = await context.Skill
            .AsNoTracking()
            .Where(s => s.SkillName == request.skillName)
            .Include(u => u.UserSkillEntities)
            .ThenInclude(us => us.AppUserEntity)
            .ToListAsync(cancellationToken: cancellationToken);
        
        var userName = skillListEntity.SelectMany(x => x.UserSkillEntities.Select(x => x.AppUserEntity.UserName)).ToList();
        List<AppUserEntity> userList = [];
        
        foreach (var item in userName)
        {
            userList
                .Add(await context.Users
                .Include(u=>u.UserSkillsEntities)
                .ThenInclude(us=>us.SkillEntity)
                .Where(u => u.UserName == item)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken));
        }
        
        return userList;
    }
}