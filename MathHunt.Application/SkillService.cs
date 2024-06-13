using MathHunt.Core.Abstraction.IRepositories;
using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;


namespace MathHunt.Application;

public class SkillService(ISkillRepository repository) : ISkillService
{
    public async Task<List<Skill>> GetUserSkill()
    {
        return await repository.Get();
    }

    public async Task<List<Skill>> GetUsersBySkillName(string skillName)
    {
        return await repository.GetUsersBySkillName(skillName);
    }
    
    public async Task<Guid> CreateUserSkill(Skill skill)
    {
        return await repository.Create(skill);
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