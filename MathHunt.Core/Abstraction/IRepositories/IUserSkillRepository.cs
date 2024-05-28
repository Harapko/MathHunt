using MathHunt.Core.Models;

namespace MathHunt.Core.Abstraction.IRepositories;

public interface IUserSkillRepository
{
    Task<List<UserSkill>> Get();
    Task<List<UserSkill>> GetByName(string skillName);
    Task<Guid> Create(UserSkill userSkill);
    Task<Guid> Update(Guid id, string skillName);
    Task<Guid> Delete(Guid id);
    Task<string> AddToUser(string emailId ,string skillName);
}