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

    [HttpGet]
    [Route("/getCompanyByUser/{userId}")]
    public async Task<ActionResult<List<GETCompanyByUserResponse>>> GetCompanyByUser(string userId)
    {
        var company = await service.GetCompanyByUser(userId);
        var response = company.Select(c => new GETCompanyByUserResponse(c.Id, c.TradeName, c.DataStart, c.DataEnd,
            c.PositionUser, c.DescriptionUsersWork,
            c.Link, c.AppUserId, c.CompanySkills.Select(cs => cs.Skill.SkillName).ToArray())).ToList();
        return Ok(response);
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
            request.descriptionUsersWork,
            request.link,
            request.appUserId,
            []
        );

        if (!string.IsNullOrWhiteSpace(error))
        {
            return BadRequest(error);
        }

        var companyId = await service.CreateCompany(company);
        return companyId;
    }
    
    [HttpPost]
    [Route("/addSkillToCompany")]
    public async Task<ActionResult<Guid>> AddSkillToCompany([FromBody] POSTAddSkillToCompanyRequest request)
    {
        var res = await service.AddSkillToCompany(request.companyId, request.skillId);
        return Ok(res);
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
            request.descriptionUsersWork,
            request.link,
            request.appUserId,
            []
        );

        if (!string.IsNullOrWhiteSpace(error))
        {
            return BadRequest(error);
        }
        
        var result = await service.UpdateCompany(company, companyId);
        return companyId;
    }

    [HttpPut]
    [Route("/updateCompanySkills")]
    public async Task<ActionResult<Guid>> UpdateCompanySkills(Guid companyId, Guid oldSkillId, Guid newSkillId)
    {
        var res = await service.UpdateCompanySkills(companyId, oldSkillId, newSkillId);
        return Ok(res);
    }

    [HttpDelete("/deleteCompany/{companyId:guid}")]
    public async Task<ActionResult<Guid>> Delete(Guid companyId)
    {
        var result = await service.DeleteCompany(companyId);
        return companyId;
    }
}