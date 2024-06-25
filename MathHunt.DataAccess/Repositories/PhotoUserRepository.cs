using Google.Cloud.Storage.V1;
using MathHunt.Core.Abstraction.IRepositories;
using Microsoft.AspNetCore.Http;

namespace MathHunt.DataAccess.Repositories;

public class PhotoUserRepository : IPhotoUserRepository
{
    public async Task<string> CreatePhoto(IFormFile photo, string appUserId)
    {
        if (photo == null)
        {
            return "null";
        }

        var client = await StorageClient.CreateAsync();
        byte[] fileBytes;
        using (var memoryStream = new MemoryStream())
        {
            await photo.CopyToAsync(memoryStream);
            fileBytes = memoryStream.ToArray();
        }

        var obj = await client.UploadObjectAsync(
            "math-hunt",
            appUserId,
            "image/png",
            new MemoryStream(fileBytes));
        
        return appUserId;
    }

    public async Task<string> UpdatePhoto(IFormFile path, string appUserId)
    {
        await DeletePhoto(appUserId);
        await CreatePhoto(path, appUserId);
        return appUserId;
    }

    public async Task<string> DeletePhoto(string appUserId)
    {
        try
        {
            var client = await StorageClient.CreateAsync();
            var stream = new MemoryStream();
            var objFromCloud = await client.DownloadObjectAsync("math-hunt", appUserId, stream);
            if (objFromCloud == null)
            {
                return appUserId;
            }
            else
            {
                await client.DeleteObjectAsync(objFromCloud);
                return appUserId;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return null;
    }
}