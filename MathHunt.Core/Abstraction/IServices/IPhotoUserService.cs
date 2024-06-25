using Microsoft.AspNetCore.Http;

namespace MathHunt.Core.Abstraction.IServices;

public interface IPhotoUserService
{
    Task<string> CreatePhoto(IFormFile photo, string appUserId);
    Task<string> UpdatePhoto(IFormFile path, string appUserId);
    Task<string> DeletePhoto(string appUserId);
}