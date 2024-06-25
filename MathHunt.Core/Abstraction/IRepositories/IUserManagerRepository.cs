using MathHunt.Core.Models;
using Microsoft.AspNetCore.Http;

namespace MathHunt.Core.Abstraction.IRepositories;

public interface IUserManagerRepository
{
    Task<List<UserSkill>> GetUserSkills(string userName);
    Task<string> AddToUser(string userName, string skillName, string proficiencyLevel);
    Task<string> UpdateSkill(string userId, Guid oldSkillId, Guid newSkillId, string proficiencyLevel);
    Task<string> DeleteSkill(string userId, string skillName);
}