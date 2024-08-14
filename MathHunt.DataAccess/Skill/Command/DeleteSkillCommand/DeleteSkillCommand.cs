using MediatR;

namespace MathHunt.DataAccess.Skill.Command.DeleteSkillCommand;

public class DeleteSkillCommand(Guid id) : IRequest<Guid>
{
    public Guid Id { get; } = id;
}