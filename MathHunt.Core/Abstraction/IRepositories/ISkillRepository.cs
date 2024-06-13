using MathHunt.Core.Models;

namespace MathHunt.Core.Abstraction.IRepositories;

public interface ISkillRepository
{
    Task<List<Skill>> Get();
    Task<List<Skill>> GetUsersBySkillName(string skillName);
    Task<Guid> Create(Skill skill);
    Task<Guid> Update(Guid id, string skillName);
    Task<Guid> Delete(Guid id);
}