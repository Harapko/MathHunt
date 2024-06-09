namespace MathHunt.Core.Abstraction.IRepositories;

public interface IUserManagerRepository
{
    Task<List<string>> GetUserSkills(string userName);
    Task<string> AddToUser(string userName, string skillName);
    Task<bool> DeleteSkill(string userName, string skillName);
}