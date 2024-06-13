using MathHunt.Contracts.Identity;
using MathHunt.Contracts.Role;
using MathHunt.Contracts.Skill;
using MathHunt.Core.Abstraction.IServices;
using Microsoft.AspNetCore.Mvc;

namespace MathHunt.Controllers;

public class UserManagerController(IUserManagerService service) : ControllerBase
{
    [HttpGet]
    [Route("/getSkillByUser/{userName}")]
    public async Task<ActionResult<GETUserSkillResponse>> GetSkillByUser(string userName)
    {
        var user = await service.GetSkillByUser(userName);
        var resp = user.Select(us => new GETUserSkillResponse(us.Skill.SkillName, us.ProficiencyLevel)).ToList();
        return Ok(resp);
    }
    
    [HttpPost("/addSkillToUser")]
    public async Task<ActionResult> AddSkillToUser([FromBody] POSTAddSkillToUserRequest skillToUserRequest)
    {
        var result = await service.AddSkillToUser(skillToUserRequest.userName, skillToUserRequest.skillName, skillToUserRequest.proficiencyLevel);
        return Ok(result);
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

    [HttpDelete("/deleteUsersSkill/{userId}/{skillId:guid}")]
    public async Task<ActionResult> DeleteUserSkills(string userId, Guid skillId)
    {
        var result = await service.DeleteSkill(userId, skillId);
        return Ok(result);
    }
    
    [HttpDelete("/deleteUserPhotos/{id:guid}")]
    public async Task<ActionResult> DeleteUserPhotos(Guid id)
    {
        var result = await service.DeletePhoto(id);
        return Ok(result);
    }
}