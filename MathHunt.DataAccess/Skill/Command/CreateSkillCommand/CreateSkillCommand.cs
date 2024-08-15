using MediatR;

namespace MathHunt.DataAccess.Skill.Command.CreateSkillCommand;

public class CreateSkillCommand(string skillName) : IRequest<Guid>
{
    public Guid Id { get; } = Guid.NewGuid();
    public string SkillName { get; } = skillName;
}