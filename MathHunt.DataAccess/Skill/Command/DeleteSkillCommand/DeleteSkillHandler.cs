using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MathHunt.DataAccess.Skill.Command.DeleteSkillCommand;

public class DeleteSkillHandler(AppDbContext context) : IRequestHandler<DeleteSkillCommand, Guid>
{
    public async Task<Guid> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        await context.Skill
            .Where(s => s.Id == request.Id)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
        return request.Id;
    }
}