using MathHunt.Core.Abstraction.IRepositories;
using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;
using Microsoft.AspNetCore.Http;

namespace MathHunt.Application;

public class UserManagerService(IUserManagerRepository repository) : IUserManagerService
{
    public async Task<List<UserSkill>> GetSkillByUser(string userName)
    {
        return await repository.GetUserSkills(userName);
    }

    public async Task<string> AddSkillToUser(string userName, string skillName, string proficiencyLevel)
    {
        return await repository.AddToUser(userName ,skillName, proficiencyLevel);
    }

    public async Task<string> UpdateUsersSkill(string userId, Guid oldSkillId, Guid newSkillId, string proficiencyLevel)
    {
        return await repository.UpdateSkill(userId, oldSkillId, newSkillId, proficiencyLevel);
    }

    public async Task<string> DeleteSkill(string userId, string skillName)
    {
        return await repository.DeleteSkill(userId, skillName);
    }
    
}