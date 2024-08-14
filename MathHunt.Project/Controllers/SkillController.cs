using MathHunt.Contracts.Skill;
using MathHunt.Core.Models;
using MathHunt.DataAccess.Skill.Command.CreateSkillCommand;
using MathHunt.DataAccess.Skill.Command.DeleteSkillCommand;
using MathHunt.DataAccess.Skill.Command.UpdateSkillCommand;
using MathHunt.DataAccess.Skill.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MathHunt.Controllers;

[ApiController]
public class SkillController(IMediator mediator) : ControllerBase
{
    [HttpGet("/getSkill")]
    public async Task<ActionResult<List<GETAllSkillResponse>>> GetSkills()
    {
        var result = await mediator.Send(new GetAllSkillQuery());
        var response = result.Select(s => new GETAllSkillResponse(s.Id, s.SkillName)).ToList();

        return response;
    }

    [HttpGet("/getUsersBySkill")]
    public async Task<ActionResult<List<GETUsersBySkillResponse>>> GetUsersBySkill(string skillName)
    {
        var result = await mediator.Send(new GetUserBySkillNameQueries(skillName));
        var response = result.Select(u =>
            new GETUsersBySkillResponse(
                u.Id, 
                u.UserName, 
                u.UserSkillsEntities.Select(us => us.ProficiencyLevel).FirstOrDefault()));

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

        // var skillId = await service.CreateUserSkill(skill);
        var result = await mediator.Send(new CreateSkillCommand(skill));
        return Ok(result);
    }


    [HttpPut("/editSkill/{id:guid}")]
    public async Task<ActionResult<Guid>> EditSkill(Guid id, string skillName)
    {
        var result = await mediator.Send(new UpdateSkillCommand(id, skillName));
        return Ok(new {message = $"Skill {result} was updated on {skillName}"});
    }

    [HttpDelete("/deleteSkill/{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteSkill(Guid id)
    {
        var result = await mediator.Send(new DeleteSkillCommand(id));
        return Ok(new {message = $"Skill {result} was deleted"});
    }
}