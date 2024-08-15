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
[Route("[action]")]
public class SkillController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<GETAllSkillResponse>>> GetSkills()
    {
        var result = await mediator.Send(new GetAllSkillQuery());
        var response = result.Select(s => new GETAllSkillResponse(s.Id, s.SkillName)).ToList();

        return response;
    }

    [HttpGet]
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

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateSkill([FromBody] CreateSkillCommand command)
    {
        var result = await mediator.Send(new CreateSkillCommand(command.SkillName));
        return Ok(result);
    }


    [HttpPut]
    public async Task<ActionResult<Guid>> EditSkill([FromBody] UpdateSkillCommand command)
    {
        var result = await mediator.Send(new UpdateSkillCommand(command.Id, command.SkillName));
        return Ok(new {message = $"Skill {result} was updated on {command.SkillName}"});
    }

    [HttpDelete]
    public async Task<ActionResult<Guid>> DeleteSkill([FromBody] DeleteSkillCommand command)
    {
        var result = await mediator.Send(new DeleteSkillCommand(command.Id));
        return Ok(new {message = $"Skill {result} was deleted"});
    }
}