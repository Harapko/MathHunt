using MathHunt.Core.Abstraction.IRepositories;
using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;
using Microsoft.AspNetCore.Http;

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

    public async Task<PhotoUser> CreateUsersPhoto(IFormFile titlePhoto, string appUserId)
    {
        return await repository.CreatePhoto(titlePhoto, appUserId);
    }

    public async Task<Guid> UpdatePhoto(Guid id, IFormFile path, string appUserId)
    {
        return await repository.UpdatePhoto(id, path, appUserId);
    }

    public async Task<Guid> DeletePhoto(Guid id)
    {
        return await repository.DeletePhoto(id);
    }
}