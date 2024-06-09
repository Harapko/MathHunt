using MathHunt.Core.Abstraction.IRepositories;
using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;

namespace MathHunt.Application;

public class CompanyService(ICompanyRepository repository) : ICompanyService
{
    public async Task<List<Company>> GetCompany()
    {
        return await repository.Get();
    }

    public async Task<List<Company>> GetCompanyByUser(string userId)
    {
        return await repository.GetByUser(userId);
    }

    public async Task<Guid> CreateCompany(Company company)
    {
        return await repository.Create(company);
    }

    public async Task<Guid> UpdateCompany(Company company, Guid companyId)
    {
        return await repository.Update(company, companyId);
    }

    public async Task<Guid> DeleteCompany(Guid companyId)
    {
        return await repository.Delete(companyId);
    }
}