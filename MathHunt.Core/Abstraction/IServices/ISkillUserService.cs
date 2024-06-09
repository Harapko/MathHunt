using MathHunt.Core.Models;

namespace MathHunt.Core.Abstraction.IServices;

public interface ISkillUserService
{
    Task<List<UserSkill>> GetUserSkill();
    Task<List<UserSkill>> GetUsersBySkillName(string skillName);

    Task<Guid> CreateUserSkill(UserSkill userSkill);
    Task<Guid> UpdateUserSkill(Guid id, string skillName);
    Task<Guid> DeleteUserSkill(Guid id);
}