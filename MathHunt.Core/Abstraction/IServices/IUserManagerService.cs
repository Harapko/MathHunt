using MathHunt.Core.Models;
using Microsoft.AspNetCore.Http;

namespace MathHunt.Core.Abstraction.IServices;

public interface IUserManagerService
{
    Task<List<UserSkill>> GetSkillByUser(string userName);
    Task<string> AddSkillToUser(string userName, string skillName, string proficiencyLevel);
    Task<string> UpdateUsersSkill(string userId, Guid oldSkillId, Guid newSkillId, string proficiencyLevel);
    Task<string> DeleteSkill(string userId, string skillName);
}