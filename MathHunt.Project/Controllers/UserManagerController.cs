using MathHunt.Contracts.Role;
using MathHunt.Core.Abstraction.IServices;
using Microsoft.AspNetCore.Mvc;

namespace MathHunt.Controllers;

public class UserManagerController(IUserManagerService service) : ControllerBase
{
    [HttpGet]
    [Route("/getSkillByUser")]
    public async Task<ActionResult> GetSkillByUser(string userName)
    {
        var user = await service.GetSkillByUser(userName);
        return  Ok(user);
    }
    
    [HttpPost("/addSkillToUser")]
    public async Task<ActionResult> AddSkillToUser([FromBody] POSTAddSkillToUserRequest skillToUserRequest)
    {
        var result = await service.AddSkillToUser(skillToUserRequest.userName, skillToUserRequest.skillName);
        return Ok(result);
    }

    [HttpDelete("/deleteUsersSkill/{userName}/{skillName}")]
    public async Task<ActionResult> DeleteUserSkills(string userName, string skillName)
    {
        var result = await service.DeleteSkill(userName, skillName);
        return Ok(result);
    }
}