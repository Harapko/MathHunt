using MathHunt.Core.Models;
using Microsoft.AspNetCore.Http;

namespace MathHunt.Core.Abstraction.IServices;

public interface IUserManagerService
{
    Task<List<string>> GetSkillByUser(string userName);
    Task<string> AddSkillToUser(string userName, string skillName);
    Task<bool> DeleteSkill(string userName, string skillName);
    Task<PhotoUser> CreateUsersPhoto(IFormFile titlePhoto, string appUserId);
    Task<Guid> UpdatePhoto(Guid id, IFormFile path, string appUserId);
    Task<Guid> DeletePhoto(Guid id);
}