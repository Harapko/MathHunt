using MathHunt.Core.Abstraction.IRepositories;
using MathHunt.Core.Abstraction.IServices;

namespace MathHunt.Application;

public class UserManagerService(IUserManagerRepository repository) : IUserManagerService
{
    public async Task<List<string>> GetSkillByUser(string userName)
    {
        return await repository.GetUserSkills(userName);
    }

    public async Task<string> AddSkillToUser(string userName, string skillName)
    {
        return await repository.AddToUser(userName ,skillName);
    }

    public async Task<bool> DeleteSkill(string userName, string skillName)
    {
        return await repository.DeleteSkill(userName, skillName);
    }
}