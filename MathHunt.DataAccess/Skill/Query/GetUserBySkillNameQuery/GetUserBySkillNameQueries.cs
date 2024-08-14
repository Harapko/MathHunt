using MathHunt.Core.Models;
using MathHunt.DataAccess.Entities;
using MediatR;

namespace MathHunt.DataAccess.Skill.Queries;

public class GetUserBySkillNameQueries(string skillName) : IRequest<List<AppUserEntity>>
{
    public string skillName { get; } = skillName;
}