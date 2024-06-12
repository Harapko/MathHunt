using MathHunt.Contracts.Skill;
using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace MathHunt.Controllers;

[ApiController]
[Route("[controller]")]
public class UserSkillController(
    ISkillUserService service) : ControllerBase
{
    [HttpGet]
    [Route("/getSkill")]
    public async Task<ActionResult<List<UserSkill>>> GetSkills()
    {
        var skillList = await service.GetUserSkill();
        var response = skillList.Select(s => new GETAllSkillResponse(s.Id, s.SkillName)).ToList();
        return Ok(response);
    }

    [HttpGet("/getUsersBySkill")]
    public async Task<ActionResult> GetUserBySkill(string skillName)
    {
        var skill = await service.GetUsersBySkillName(skillName);
        
        return Ok(skill);
    }

    [HttpPost("/createSkill")]
    public async Task<ActionResult<Guid>> CreateSkill(string skillName)
    {
        var (skill, error) = UserSkill.Create(
            Guid.NewGuid(),
            skillName,
            []);

        if (!string.IsNullOrWhiteSpace(error))
        {
            return BadRequest(error);
        }

        var skillId = await service.CreateUserSkill(skill);
        return Ok(skillId);
    }
    

    [HttpPut("/editSkill/{id:guid}")]
    public async Task<ActionResult<Guid>> EditSkill(Guid id, string skillName)
    {
        var skillId = await service.UpdateUserSkill(id, skillName);
        return Ok(skillId);
    }

    [HttpDelete("/deleteSkill/{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteSkill(Guid id)
    {
        var skillId = await service.DeleteUserSkill(id);
        return Ok(skillId);
    }
}