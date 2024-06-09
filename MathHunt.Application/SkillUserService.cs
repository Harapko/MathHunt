using MathHunt.Core.Abstraction.IRepositories;
using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;


namespace MathHunt.Application;

public class SkillUserService(ISkillUserRepository repository) : ISkillUserService
{
    public async Task<List<UserSkill>> GetUserSkill()
    {
        return await repository.Get();
    }

    public async Task<List<UserSkill>> GetUsersBySkillName(string skillName)
    {
        return await repository.GetUsersBySkillName(skillName);
    }
    
    public async Task<Guid> CreateUserSkill(UserSkill userSkill)
    {
        return await repository.Create(userSkill);
    }
    
    public async Task<Guid> UpdateUserSkill(Guid id, string skillName)
    {
        return await repository.Update(id, skillName);
    }
    
    public async Task<Guid> DeleteUserSkill(Guid id)
    {
        return await repository.Delete(id);
    }

}