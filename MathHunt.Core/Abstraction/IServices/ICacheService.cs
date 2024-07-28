namespace MathHunt.Core.Abstraction.IServices;

public interface ICacheService
{
    Task<T> GetData<T>(string key);
    Task<bool> SetData<T>(string key, T value);
    Task<bool> AddItemToCollection<T>(string key, T newItem);
    Task<bool> UpdateItemToCollection<T>(string key, Predicate<T> match, T updatedItem);
    Task<bool> DeleteItemToCollection<T>(string key, Func<T, bool> match);
    Task<object> RemoveData(string key);
}