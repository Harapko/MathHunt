using Google.Cloud.Storage.V1;
using MathHunt.Contracts.Identity;
using MathHunt.Contracts.Role;
using MathHunt.Contracts.Skill;
using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace MathHunt.Controllers;

public class UserManagerController(IUserManagerService service) : ControllerBase
{
    [HttpGet]
    [Route("/getSkillByUser/{userName}")]
    public async Task<ActionResult<GETUserSkillResponse>> GetSkillByUser(string userName)
    {
        var user = await service.GetSkillByUser(userName);
        var response = user.Select(us => new GETUserSkillResponse(us.SkillId, us.Skill.SkillName, us.ProficiencyLevel)).ToList();
        return Ok(response);
    }
    
    [HttpPost("/addSkillToUser")]
    public async Task<ActionResult<string>> AddSkillToUser([FromBody] POSTAddSkillToUserRequest skillToUserRequest)
    {
        var result = await service.AddSkillToUser(skillToUserRequest.userName, skillToUserRequest.skillName, skillToUserRequest.proficiencyLevel);
        return Ok(new { message = $"Add Skill to user successfully {result}" });
    }

    [HttpPut("/updateUsersSkill/")]
    public async Task<ActionResult> UpdateUsersSkill([FromBody] PUTUpdateUsersSkillRequest request)
    {
        var res = await service.UpdateUsersSkill(request.userId, request.oldSkillId, request.newSkillId, request.proficiencyLevel);
        return Ok(res);
    }

    [HttpDelete("/deleteUsersSkill/{userId}/{skillName}")]
    public async Task<ActionResult<string>> DeleteUserSkills([FromRoute] DELETEUserSkillRequest request)
    {
        var result = await service.DeleteSkill(request.userId, request.skillName);
        return Ok(new { message = $"Delete user skill successfully {result}" });
    } 
    
    
}