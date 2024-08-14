using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MathHunt.DataAccess.Skill.Command.UpdateSkillCommand;

public class UpdateSkillHandler(AppDbContext context) : IRequestHandler<UpdateSkillCommand, Guid>
{
    public async Task<Guid> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
    {
        await context.Skill
            .Where(s => s.Id == request.Id)
            .ExecuteUpdateAsync(set => set
                .SetProperty(s => s.SkillName, request.SkillName), cancellationToken: cancellationToken);

        return request.Id;
    }
}