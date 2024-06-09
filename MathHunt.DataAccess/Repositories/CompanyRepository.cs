using MathHunt.Core.Abstraction.IRepositories;
using MathHunt.Core.Models;
using MathHunt.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MathHunt.DataAccess.Repositories;

public class CompanyRepository(AppDbContext context) : ICompanyRepository
{
    public async Task<List<Company>> Get()
    {
        var companyEntity = await context.Company
            .AsNoTracking()
            .ToListAsync();

        var company = companyEntity
            .Select(c => Company.Create(c.Id, c.TradeName, c.DataStart, c.DataEnd, c.PositionUser, c.DescriptionUsersWork, c.AppUserId)
                .company)
            .ToList();

        return company;
    }

    public async Task<List<Company>> GetByUser(string userId)
    {
        var companyEntityByUserId = await context.Company
            .AsNoTracking()
            .Where(c => c.AppUserId == userId)
            .ToListAsync();

        var companyByUserId = companyEntityByUserId
            .Select(c => Company.Create(c.Id, c.TradeName, c.DataStart, c.DataEnd, c.PositionUser, c.DescriptionUsersWork, c.AppUserId)
                .company)
            .ToList();
        return companyByUserId;
    }

    public async Task<Guid> Create(Company company)
    {
        var companyEntity = new CompanyEntity
        {
            Id = company.Id,
            TradeName = company.TradeName,
            DataStart = company.DataStart,
            DataEnd = company.DataEnd,
            PositionUser = company.PositionUser,
            DescriptionUsersWork = company.DescriptionUsersWork,
            AppUserId = company.AppUserId
        };

        await context.Company.AddAsync(companyEntity);
        await context.SaveChangesAsync();
        return companyEntity.Id;
    }

    public async Task<Guid> Update(Company company, Guid companyId)
    {
        await context.Company
            .ExecuteUpdateAsync(set => set
                .SetProperty(c => c.TradeName, company.TradeName)
                .SetProperty(c => c.DataStart, company.DataStart)
                .SetProperty(c=>c.DataEnd, company.DataEnd)
                .SetProperty(c=>c.PositionUser, company.PositionUser)
                .SetProperty(c=>c.DescriptionUsersWork, company.DescriptionUsersWork));

        return companyId;
    }

    public async Task<Guid> Delete(Guid companyId)
    {
        await context.Company
            .Where(c => c.Id == companyId)
            .ExecuteDeleteAsync();

        return companyId;
    }
}