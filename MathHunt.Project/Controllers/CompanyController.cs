using MathHunt.Contracts.CompanyContract;
using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace MathHunt.Controllers;

[ApiController]
public class CompanyController(ICompanyService service) : ControllerBase
{
    [HttpGet]
    [Route("/getCompany")]
    public async Task<ActionResult<List<Company>>> GetCompany()
    {
        var companyList = await service.GetCompany();
        return companyList;
    }

    [HttpPost]
    [Route("/createCompany")]
    public async Task<ActionResult<Guid>> CreateCompany([FromBody] POSTCreateCompanyRequest request)
    {
        var (company, error) = Company.Create(
            Guid.NewGuid(),
            request.tradeName,
            request.dataStart,
            request.dataEnd,
            request.positionUser,
            request.DescriptionUsersWork,
            request.appUserId
        );

        if (!string.IsNullOrWhiteSpace(error))
        {
            return BadRequest(error);
        }

        var companyId = await service.CreateCompany(company);
        return companyId;
    }

    [HttpPut("/updateCompany/{companyId:guid}")]
    public async Task<ActionResult<Guid>> UpdateCompany([FromBody] PUTUpdateCompanyRequest request, Guid companyId)
    {
        var (company, error) = Company.Create(
            Guid.Empty,
            request.tradeName,
            request.dataStart,
            request.dataEnd,
            request.positionUser,
            request.DescriptionUsersWork,
            request.appUserId
        );

        if (!string.IsNullOrWhiteSpace(error))
        {
            return BadRequest(error);
        }
        
        var result = await service.UpdateCompany(company, companyId);
        return companyId;
    }

    [HttpDelete("/deleteCompany/{companyId:guid}")]
    public async Task<ActionResult<Guid>> Delete(Guid companyId)
    {
        var result = await service.DeleteCompany(companyId);
        return companyId;
    }
}