using MathHunt.DataAccess.Entities;
using MediatR;

namespace MathHunt.DataAccess.Skill.Command.CreateSkillCommand;

public class CreateSkillHandler(AppDbContext context) : IRequestHandler<CreateSkillCommand, Guid>
{
    public async Task<Guid> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
    {
        var userSkillEntity = new SkillEntity()
        {
            Id = request.skill.Id,
            SkillName = request.skill.SkillName,
        };

        await context.Skill.AddAsync(userSkillEntity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return userSkillEntity.Id;
    }
}