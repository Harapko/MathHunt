using MathHunt.Core.Models;

namespace MathHunt.Core.Abstraction.IServices;

public interface ISkillService
{
    Task<List<Skill>> GetUserSkill();
    Task<List<Skill>> GetUsersBySkillName(string skillName);

    Task<Guid> CreateUserSkill(Skill skill);
    Task<Guid> UpdateUserSkill(Guid id, string skillName);
    Task<Guid> DeleteUserSkill(Guid id);
}