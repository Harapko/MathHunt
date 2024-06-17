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
        var response = new POSTAddSkillToUserResponse(result);
        return Ok(response);
    }

    [HttpPut("/updateUsersSkill/")]
    public async Task<ActionResult> UpdateUsersSkill([FromBody] PUTUpdateUsersSkillRequest request)
    {
        var res = await service.UpdateUsersSkill(request.userId, request.oldSkillId, request.newSkillId, request.proficiencyLevel);
        return Ok(res);
    }

    [HttpDelete("/deleteUsersSkill/{userId}/{skillName}")]
    public async Task<ActionResult<DELETEUsersSkillResponse>> DeleteUserSkills([FromRoute] DELETEUserSkillRequest request)
    {
        var result = await service.DeleteSkill(request.userId, request.skillName);
        var response = new DELETEUsersSkillResponse(result);
        return Ok(response);
    } 
    
    [HttpPost]
    [Route("/createUsersPhoto")]
    public async Task<ActionResult> CreateUsersPhoto(IFormFile file, string appUserId)
    {
        var result = await service.CreateUsersPhoto(file, appUserId);
        return Ok(result);
    }

    [HttpPut("/updateUserPhoto/{id:guid}")]
    public async Task<ActionResult> UpdateUserPhoto(Guid id, IFormFile path, string appUserId)
    {
        var result = await service.UpdatePhoto(id, path, appUserId);
        return Ok(result);
    }

    
    [HttpDelete("/deleteUserPhotos/{id:guid}")]
    public async Task<ActionResult> DeleteUserPhotos(Guid id)
    {
        var result = await service.DeletePhoto(id);
        return Ok(result);
    }
}