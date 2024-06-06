using MathHunt.Contracts.Identity;
using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;
using MathHunt.DataAccess.Entities;
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
            .Select(u => new GETAllUserResponse(u.UserName, u.UserSurname, u.Email, u.PhoneNumber, u.EnglishLevel, u.DescriptionSkill, u.Role, u.UserSkills
                .Select(s=>s.SkillName)
                .ToArray()));

        return Ok(response);
    }

    
    [HttpGet]
    [Route("/getSkillsUser")]
    public async Task<ActionResult> GetSkillByUser(string userName)
    {
        var user = await userService.GetUsersSkill(userName);
        return  Ok(user);
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
    
    [HttpPost]
    [Route("/registerUser")]
    public async Task<ActionResult> Register([FromBody] POSTRegisterCustomRequest postRegisterRequest)
    {

        var (user, error) = AppUser.Create(
            postRegisterRequest.name,
            postRegisterRequest.surname,
            postRegisterRequest.email,
            postRegisterRequest.phoneNumber,
            postRegisterRequest.englishLevel,
            "",
            postRegisterRequest.role,
            []
        );
    
        var result = await userService.RegisterUser(user, postRegisterRequest.password, postRegisterRequest.role);
        return Ok(new { message = "User registered successfully!" });
        
    }

    [HttpPut]
    [Route("/updateUser")]
    public async Task<ActionResult> UpdateUser([FromBody] PUTUpdateUserRequest request)
    {
        var updateUser = AppUser.Create(
            request.userName,
            request.userSurname,
            request.email,
            request.phoneNumber,
            request.englishLevel,
            request.descriptionSkill,
            "",
            []
        ).appUser;
        
        
        var userId = await userService.UpdateUser(request.userName, updateUser);
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