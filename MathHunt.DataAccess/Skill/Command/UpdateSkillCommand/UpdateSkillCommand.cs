using MediatR;

namespace MathHunt.DataAccess.Skill.Command.UpdateSkillCommand;

public class UpdateSkillCommand(Guid id, string skillName) : IRequest<Guid>
{
    public Guid Id { get; } = id;
    public string SkillName { get; } = skillName;
}