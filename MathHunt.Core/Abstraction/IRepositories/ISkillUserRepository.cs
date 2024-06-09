using MathHunt.Core.Models;

namespace MathHunt.Core.Abstraction.IRepositories;

public interface ISkillUserRepository
{
    Task<List<UserSkill>> Get();
    Task<List<UserSkill>> GetUsersBySkillName(string skillName);
    Task<Guid> Create(UserSkill userSkill);
    Task<Guid> Update(Guid id, string skillName);
    Task<Guid> Delete(Guid id);
}