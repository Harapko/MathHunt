using MathHunt.Core.Abstraction.IRepositories;
using MathHunt.Core.Models;
using MathHunt.DataAccess;
using MathHunt.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;


public class CompanyRepository(AppDbContext context) : ICompanyRepository
{
    public async Task<List<Company>> Get()
    {
        var companyEntity = await context.Company
            .AsNoTracking()
            .Include(c=>c.CompanySkill)
            .ThenInclude(cs=>cs.Skill)
            .ToListAsync();

        var company = companyEntity
            .Select(c => Company.Create(c.Id, c.TradeName, c.DataStart, c.DataEnd, c.PositionUser, c.DescriptionUsersWork, c.Link, c.AppUserId, c.CompanySkill
                    .Select(cs => CompanySkill.Create(cs.CompanyId, null, cs.SkillId, Skill.Create(cs.Skill.Id, cs.Skill.SkillName, []).userSkill).companySkill)
                    .ToList())
                .company)
            .ToList();

        return company;
    }

    public async Task<List<Company>> GetByUser(string userId)
    {
        var company = await Get();
        var userCompany = company
            .Where(c => c.AppUserId == userId)
            .ToList();
        return userCompany;
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
            Link = company.Link,
            DescriptionUsersWork = company.DescriptionUsersWork,
            AppUserId = company.AppUserId
        };

        await context.Company.AddAsync(companyEntity);
        await context.SaveChangesAsync();
        return companyEntity.Id;
    }

    public async Task<Guid> AddSkillToCompany(Guid companyId, Guid skillId)
    {
        var companySkill = new CompanySkillEntity
        {
            CompanyId = companyId,
            SkillId = skillId
        };
        await context.CompanySkill.AddAsync(companySkill);
        await context.SaveChangesAsync();

        return companyId;
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

    public async Task<Guid> UpdateSkill(Guid companyId, Guid oldSkillId, Guid newSkillId)
    {
        await context.CompanySkill
            .Where(cs => cs.CompanyId == companyId)
            .Where(cs => cs.SkillId == oldSkillId)
            .ExecuteUpdateAsync(set => set
                .SetProperty(cs => cs.SkillId, newSkillId));

        return companyId;
    }

    public async Task<Guid> Delete(Guid companyId)
    {
        await context.Company
            .Where(c => c.Id == companyId)
            .ExecuteDeleteAsync();

        return companyId;
    }

    public async Task<Guid> DeleteSkill(Guid companyId, string skillName)
    {
        await context.CompanySkill
            .Where(cs => cs.CompanyId == companyId)
            .Where(cs => cs.Skill.SkillName == skillName)
            .ExecuteDeleteAsync();

        return companyId;
    }
}