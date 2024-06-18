using System.Security.Claims;
using MathHunt.Contracts.CompanyContract;
using MathHunt.Contracts.Identity;
using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace MathHunt.Controllers;

[ApiController]

public class AppUserController(IAppUserService userService) : ControllerBase
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
                    .Select(p => p.Path).FirstOrDefault()))
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
            var response = new GETUserByIdResponse(user.Id,
                user.UserName,
                user.UserSurname,
                user.Email,
                user.PhoneNumber,
                user.EnglishLevel,
                user.GitHubLink,
                user.DescriptionSkill,
                user.Role,
                user.PhotoUsers.Select(p => p.Path).FirstOrDefault());
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
            user.PhotoUsers.Select(p => p.Path).FirstOrDefault(),
            user.Companies.ToArray(),
            user.UserSkills
                .Select(us=> new GETUserSkillResponse(us.SkillId, us.Skill.SkillName, us.ProficiencyLevel))
                .ToArray()
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
            [],
            [],
            []
        );
    
        var result = await userService.RegisterUser(user, postRegisterRequest.password, postRegisterRequest.role);
        return Ok(new { message = "User registered successfully!" });
        
    }
    
    [HttpPost]
    [Route("/banUser")]
    public async Task<ActionResult> BanUser(string userName)
    {
        return Ok(await userService.BanUser(userName));
    }

    [HttpPut("updateUser/{userName}")]
    public async Task<ActionResult> UpdateUser([FromBody] PUTUpdateUserRequest request, string userName)
    {
        var updateUser = AppUser.Create(
            Guid.NewGuid().ToString(),
            request.userName,
            request.userSurname,
            request.email,
            request.phoneNumber,
            request.englishLevel,
            request.descriptionSkill,
            "",
            "",
            [],
            [],
            []
        ).appUser;
        
        
        var userId = await userService.UpdateUser(userName, updateUser);
        return Ok(userId);
    }
    
    // [HttpGet]
    // [Route("/logout")]
    // public async Task<ActionResult> Logout()
    // {
    //     // Выполните логику выхода пользователя
    //     var result =  userRepository.Logout();
    //     return Ok(new { message = "Logged out successfully", result });
    // }
    
    [HttpDelete]
    [Route("/deleteUser")]
    
    public async Task<ActionResult> Delete(string userName)
    {
        var result = await userService.DeleteUser(userName);
        return Ok(result);
    }


}