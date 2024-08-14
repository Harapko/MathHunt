using MediatR;

namespace MathHunt.DataAccess.Skill.Command.CreateSkillCommand;

public class CreateSkillCommand(Core.Models.Skill skill) : IRequest<Guid>
{
    public Core.Models.Skill skill { get; } = skill;
}