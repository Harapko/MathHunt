using System.Security.Claims;
using MathHunt.Contracts.Identity;
using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;
using MathHunt.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MathHunt.Controllers;

[ApiController]

public class AppUserController(
    IAppUserService userService,
    ICacheService cacheService,
    ILogger<AppUserController> logger) : ControllerBase
{
    [HttpGet]
    [Route("/getUser")]
    public async Task<ActionResult<List<GETAllUserResponse>>> GetUser()
    {
        var userList = await userService.GetAllUser();
        
        var response = userList
            .Select(u => new GETAllUserResponse(
                u.Id,
                u.UserName,
                u.UserSurname,
                u.Email,
                u.PhoneNumber, 
                u.EnglishLevel,
                u.GitHubLink,
                u.DescriptionSkill,
                u.Role, 
                u.PhotoUsers
                    .Select(p => p.Path).FirstOrDefault(),
                u.LockEnd,
                u.IsLock
                ))
            .ToList();
        
        return Ok(response);
    }
    
    
    [HttpGet]
    [Route("/getUserById/{id}")]
    public async Task<ActionResult<GETUserByIdResponse>> GetUserById(string id)
    {
        if (id.Length >= 3)
        {
            var user = await userService.GetUserById(id);
            var response = new GETUserByIdResponse(
                user.Id,
                user.UserName,
                user.UserSurname,
                user.Email,
                user.PhoneNumber,
                user.EnglishLevel,
                user.GitHubLink,
                user.DescriptionSkill,
                user.Role,
                $"https://storage.cloud.google.com/math-hunt/{user.Id}?authuser=1",
                user.LockEnd,
                user.IsLock
                );
            return Ok(response);
        }
        else
        {
            return BadRequest(new { message = "Email is null!" });
        }

    }

    [HttpGet]
    [Route("/getCurrentUser")]
    public async Task<ActionResult<GETCurrentUserResponse>> GetCurrentUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await userService.GetUserById(userId);
        var result = new GETCurrentUserResponse(
            user.Id,
            user.UserName,
            user.UserSurname,
            user.Email,
            user.PhoneNumber,
            user.EnglishLevel,
            user.GitHubLink,
            user.DescriptionSkill,
            user.Role,
            $"https://storage.cloud.google.com/math-hunt/{user.Id}?authuser=1",
            user.Companies.ToArray(),
            user.UserSkills
                .Select(us=> new GETUserSkillResponse(us.SkillId, us.Skill.SkillName, us.ProficiencyLevel))
                .ToArray(),
            user.LockEnd,
            user.IsLock
        );
        return Ok(result);
    }
    
    [HttpPost]
    [Route("/registerUser")]
    public async Task<ActionResult> Register([FromBody] POSTRegisterCustomRequest postRegisterRequest)
    {

        var (user, error) = AppUser.Create(
            Guid.NewGuid().ToString(),
            postRegisterRequest.name,
            postRegisterRequest.surname,
            postRegisterRequest.email,
            postRegisterRequest.phoneNumber,
            postRegisterRequest.englishLevel,
            "",
            "",
            postRegisterRequest.role,
            DateTime.Now,
            false,
            [],
            [],
            []
        );
    
        var result = await userService.RegisterUser(user, postRegisterRequest.password, postRegisterRequest.role);
        return Ok(new { message = "User registered successfully!" });
        
    }
    
    [HttpPost]
    [Route("/banUser/{userName}")]
    public async Task<ActionResult> BanUser([FromRoute]string userName)
    {
        return Ok( new {message = await userService.BanUser(userName)});
    }

    [HttpPut("updateUser/{userId}")]
    public async Task<ActionResult<Guid>> UpdateUser([FromBody] PUTUpdateUserRequest request, string userId)
    {
        var updateUser = AppUser.Create(
            Guid.NewGuid().ToString(),
            // request.userName,
            "",
            request.userSurname,
            request.email,
            request.phoneNumber,
            request.englishLevel,
            request.descriptionSkill,
            request.gitHubLink,
            "",
            DateTime.Now,
            true,
            [],
            [],
            []
        ).appUser;
        
        
        var userResponse = await userService.UpdateUser(userId, updateUser);
        return Ok(new { message = "User update successfully" });
    }
    
    
    [HttpDelete]
    [Route("/deleteUser")]
    
    public async Task<ActionResult> Delete(string userName)
    {
        var result = await userService.DeleteUser(userName);
        return Ok(result);
    }


}