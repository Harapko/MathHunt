using MathHunt.Core.Models;

namespace MathHunt.Core.Abstraction.IRepositories;

public interface ICompanyRepository
{
    Task<List<Company>> Get();
    Task<List<Company>> GetByUser(string userId);
    Task<Guid> Create(Company company);
    Task<Guid> AddSkillToCompany(Guid companyId, Guid skillId);
    Task<Guid> Update(Company company, Guid companyId);
    Task<Guid> UpdateSkill(Guid companyId, Guid oldSkillId, Guid newSkillId);
    Task<Guid> Delete(Guid companyId);
}