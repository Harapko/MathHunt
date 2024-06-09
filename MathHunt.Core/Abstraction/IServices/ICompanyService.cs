using MathHunt.Core.Models;

namespace MathHunt.Core.Abstraction.IServices;

public interface ICompanyService
{
    Task<List<Company>> GetCompany();
    Task<List<Company>> GetCompanyByUser(string userId);
    Task<Guid> CreateCompany(Company company);
    Task<Guid> UpdateCompany(Company company, Guid companyId);
    Task<Guid> DeleteCompany(Guid companyId);
}