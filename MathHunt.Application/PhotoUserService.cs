using MathHunt.Core.Abstraction.IRepositories;
using MathHunt.Core.Abstraction.IServices;
using Microsoft.AspNetCore.Http;

namespace MathHunt.Application;

public class PhotoUserService(IPhotoUserRepository repository) : IPhotoUserService
{
    public async Task<string> CreatePhoto(IFormFile photo, string appUserId)
    {
        return await repository.CreatePhoto(photo, appUserId);
    }

    public async Task<string> UpdatePhoto(IFormFile path, string appUserId)
    {
        return await repository.UpdatePhoto(path, appUserId);
    }

    public async Task<string> DeletePhoto(string appUserId)
    {
        return await repository.DeletePhoto(appUserId);
    }
}