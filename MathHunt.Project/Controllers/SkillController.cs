using MathHunt.Contracts.Skill;
using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace MathHunt.Controllers;

[ApiController]
public class SkillController(
    ISkillService service) : ControllerBase
{
    [HttpGet("/getSkill")]
    public async Task<ActionResult<List<GETAllSkillResponse>>> GetSkills()
    {
        var skillList = await service.GetUserSkill();
        var response = skillList.Select(s => new GETAllSkillResponse(s.Id, s.SkillName)).ToList();
        return Ok(response);
    }

    [HttpGet("/getUsersBySkill")]
    public async Task<ActionResult> GetUsersBySkill(string skillName)
    {
        var skillList = await service.GetUsersBySkillName(skillName);
        var userList = skillList.Select(s => s.UserSkills).FirstOrDefault();
        var response = userList.Select(us => new GETUsersBySkillResponse(
            us.AppUser.Id,
            us.AppUser.UserName,
            us.ProficiencyLevel
        )).ToList();

        return Ok(response);
    }

    [HttpPost("/createSkill")]
    public async Task<ActionResult<Guid>> CreateSkill(string skillName)
    {
        var (skill, error) = Skill.Create(
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