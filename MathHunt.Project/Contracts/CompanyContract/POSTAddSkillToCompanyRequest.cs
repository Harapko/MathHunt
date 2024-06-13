namespace MathHunt.Contracts.CompanyContract;

public record POSTAddSkillToCompanyRequest(
    Guid companyId,
    Guid skillId
    );