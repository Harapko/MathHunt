namespace MathHunt.Core.Abstraction.IServices;

public interface IUserManagerService
{
    Task<List<string>> GetSkillByUser(string userName);
    Task<string> AddSkillToUser(string userName, string skillName);
    Task<bool> DeleteSkill(string userName, string skillName);
}