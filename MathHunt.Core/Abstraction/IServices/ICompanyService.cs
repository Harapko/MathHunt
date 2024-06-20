using MathHunt.Core.Models;

namespace MathHunt.Core.Abstraction.IServices;

public interface ICompanyService
{
    Task<List<Company>> GetCompany();
    Task<List<Company>> GetCompanyByUser(string userId);
    Task<Guid> CreateCompany(Company company);
    Task<Guid> AddSkillToCompany(Guid companyId, Guid skillId);
    Task<Guid> UpdateCompany(Company company, Guid companyId);
    Task<Guid> UpdateCompanySkills(Guid companyId, Guid oldSkillId, Guid newSkillId);
    Task<Guid> DeleteCompany(Guid companyId);
    Task<Guid> DeleteCompanySkill(Guid companyId, string skillName);
}