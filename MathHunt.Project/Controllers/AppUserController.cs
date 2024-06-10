using System.Security.Claims;
using MathHunt.Contracts.Identity;
using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathHunt.Controllers;

[ApiController]

public class AppUserController(IAppUserService userService) : ControllerBase
{
    [Authorize]
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
                u.DescriptionSkill,
                u.Role,
                u.UserSkills
                .Select(s=>s.SkillName).ToArray()
                ));

        return Ok(response);
    }
    
    
    [HttpGet]
    [Route("/getUserByName")]
    public async Task<ActionResult<AppUser>> GetUserByName(string name)
    {
        if (name.Length >= 3)
        {
            var user = await userService.GetUserByName(name);
            var result = new GETUserByNameResponse(user.UserName, user.UserSurname, user.Email, user.PhoneNumber, user.EnglishLevel, user.DescriptionSkill, user.Role, user.UserSkills
                .Select(s=>s.SkillName).ToArray());
            return Ok(result);
        }
        else
        {
            return BadRequest(new { message = "Email is null!" });
        }
    }

    [HttpGet]
    [Route("/getCurrentUser")]
    public async Task<ActionResult<AppUser>> GetCurrentUser()
    {
        var currentUserName =  User.FindFirstValue(ClaimTypes.Name);
        var user = await userService.GetUserByName(currentUserName);
        return Ok(user);
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
            postRegisterRequest.role,
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