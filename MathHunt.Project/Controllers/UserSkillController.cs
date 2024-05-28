using MathHunt.Contracts.Role;
using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace MathHunt.Controllers;

[ApiController]
[Route("[controller]")]
public class UserSkillController(
    IUserSkillService skillService) : ControllerBase
{
    [HttpGet("/getSkill")]
    public async Task<ActionResult<List<UserSkill>>> GetSkills()
    {
        var skillList =  await skillService.GetUserSkill();
        return Ok(skillList);
    }

    [HttpPost("/createSkill")]
    public async Task<ActionResult<Guid>> CreateSkill(string skillName)
    {
        var (skill, error) = UserSkill.Create(
            Guid.NewGuid(),
            skillName
        );

        if (!string.IsNullOrWhiteSpace(error))
        {
            return BadRequest(error);
        }

        var skillId = await skillService.CreateUserSkill(skill);
        return Ok(skillId);
    }

    [HttpPut("/editSkill/{id:guid}")]
    public async Task<ActionResult<Guid>> EditSkill(Guid id,string skillName)
    {
        var skillId = await skillService.UpdateUserSkill(id, skillName);
        return Ok(skillId);
    }

    [HttpDelete("/deleteSkill/{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteSkill(Guid id)
    {
        var skillId = await skillService.DeleteUserSkill(id);
        return Ok(skillId);
    }
    
    [HttpPost("/addSkillToUser")]
    public async Task<ActionResult> AddSkillToUser([FromBody] AddSkillToUserRequest skillToUserRequest)
    {
        var result = await skillService.AddSkillToUser(skillToUserRequest.emailId, skillToUserRequest.skillName);
        return Ok(result);
    }
    
}