using MathHunt.Contracts.Role;
using MathHunt.Contracts.Skill;
using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;
using MathHunt.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MathHunt.Controllers;

[ApiController]
[Route("[controller]")]
public class UserSkillController(
    ISkillUserService service,
    AppDbContext context) : ControllerBase
{
    [HttpGet]
    [Route("/getSkill")]
    public async Task<ActionResult<List<UserSkill>>> GetSkills()
    {
        var skillList = await service.GetUserSkill();
        var response = skillList.Select(s => new AllSkillResponse(s.Id, s.SkillName)).ToList();
        return Ok(response);
    }

    [HttpGet("/getUsersSkill")]
    public async Task<ActionResult> GetUserBySkill(string skillName)
    {
        var skill = await context.UserSkill
            .Where(s => s.SkillName == skillName)
            .Include(s => s.AppUserEntities)
            .ToListAsync();
        
        

        return Ok(skill);
    }

    [HttpPost("/createSkill")]
    public async Task<ActionResult<Guid>> CreateSkill(string skillName)
    {
        var (skill, error) = UserSkill.Create(
            Guid.NewGuid(),
            skillName);

        if (!string.IsNullOrWhiteSpace(error))
        {
            return BadRequest(error);
        }
        else
        {
        }

        var skillId = await service.CreateUserSkill(skill);
        return Ok(skillId);
    }

    [HttpPost("/addSkillToUser")]
    public async Task<ActionResult> AddSkillToUser([FromBody] AddSkillToUserRequest skillToUserRequest)
    {
        var result = await service.AddSkillToUser(skillToUserRequest.userName, skillToUserRequest.skillName);
        return Ok(result);
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