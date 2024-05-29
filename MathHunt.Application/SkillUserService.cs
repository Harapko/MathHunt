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

    public async Task<List<UserSkill>> GetSkillByName(string skillName)
    {
        return await repository.GetByName(skillName);
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

    public async Task<string> AddSkillToUser(string emailId,string skillName)
    {
        return await repository.AddToUser(emailId ,skillName);
    }
    
    public async Task<List<UserSkill>> GetUsersSkill(string skillName)
    {
        return await repository.GetUsers(skillName);
    }
}