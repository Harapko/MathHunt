using MathHunt.Core.Abstraction.IRepositories;
using MathHunt.Core.Abstraction.IServices;

namespace MathHunt.Application;

public class CacheService(ICacheRepository repository) : ICacheService
{
    public async Task<T> GetData<T>(string key)
    {
        return await repository.GetData<T>(key);
    }

    public async Task<bool> SetData<T>(string key, T value)
    {
        return await repository.SetData<T>(key, value);
    }

    public async Task<bool> AddItemToCollection<T>(string key, T newItem)
    {
        return await repository.AddItemToCollection<T>(key, newItem);
    }

    public async Task<bool> UpdateItemToCollection<T>(string key, Predicate<T> match, T updatedItem)
    {
        return await repository.UpdateItemToCollection<T>(key, match, updatedItem);
    }

    public async Task<bool> DeleteItemToCollection<T>(string key, Func<T, bool> match)
    {
        return await repository.DeleteItemToCollection<T>(key, match);
    }

    public async Task<object> RemoveData(string key)
    {
        return await repository.RemoveData(key);
    }
}