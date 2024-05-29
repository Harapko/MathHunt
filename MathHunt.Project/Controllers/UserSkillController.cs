using MathHunt.Contracts.Role;
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
    [HttpGet("/getSkill")]
    public async Task<ActionResult<List<UserSkill>>> GetSkills()
    {
        var skillList =  await service.GetUserSkill();
        return Ok(skillList);
    }
    
    [HttpGet("/getUsersSkill")]
    public async Task<ActionResult> GetUserBySkill(string skillName)
    {
        var result = await context.UserSkill
            .Where(s => s.SkillName == skillName)
            .Include(s => s.AppUserEntities)
            .ToListAsync();
        
        return Ok(result);
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

        var skillId = await service.CreateUserSkill(skill);
        return Ok(skillId);
    }
    
    [HttpPost("/addSkillToUser")]
    public async Task<ActionResult> AddSkillToUser([FromBody] AddSkillToUserRequest skillToUserRequest)
    {
        var result = await service.AddSkillToUser(skillToUserRequest.emailId, skillToUserRequest.skillName);
        return Ok(result);
    }

    [HttpPut("/editSkill/{id:guid}")]
    public async Task<ActionResult<Guid>> EditSkill(Guid id,string skillName)
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