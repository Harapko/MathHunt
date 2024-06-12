using MathHunt.Core.Models;
using Microsoft.AspNetCore.Http;

namespace MathHunt.Core.Abstraction.IRepositories;

public interface IUserManagerRepository
{
    Task<List<string>> GetUserSkills(string userName);
    Task<string> AddToUser(string userName, string skillName);
    Task<bool> DeleteSkill(string userName, string skillName);
    Task<PhotoUser> CreatePhoto(IFormFile titlePhoto, string appUserId);
    Task<Guid> UpdatePhoto(Guid id, IFormFile path, string appUserId);
    Task<Guid> DeletePhoto(Guid id);
}