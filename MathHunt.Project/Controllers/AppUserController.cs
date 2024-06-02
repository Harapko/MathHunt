using MathHunt.Contracts.Identity;
using MathHunt.Contracts.Skill;
using MathHunt.DataAccess;
using MathHunt.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MathHunt.Controllers;

[ApiController]

public class AppUserController(IAppUserRepository userRepository) : ControllerBase
{
    [HttpGet]
    [Route("/getUser")]
    public async Task<ActionResult> GetUser()
    {
        var userList = await userRepository.Get();
        return Ok(userList);
    }
    
    [HttpGet]
    [Route("/getSkillsUser")]
    public async Task<ActionResult> GetSkillByUser(string userName)
    {
        var result = await userRepository.Get();
        var response = result.FirstOrDefault(s => s.UserName == userName);
    
        if (response == null)
        {
            return NotFound("User not found.");
        }

        var skillNames = response.UserSkillsEntities.Select(u => u.SkillName).ToArray();
        if (skillNames.Length == 0)
        {
            return NotFound("Skills not found.");
        }
        
        return Ok(new UsersSkillResponse(skillNames));
    }
    
    [HttpPost]
    [Route("/registerUser")]
    public async Task<ActionResult> Register([FromBody] RegisterCustomRequest registerRequest)
    {
        AppUserEntity user = new()
        {
            UserName = registerRequest.name,
            UserSurname = registerRequest.surname,
            Email = registerRequest.email,
            PhoneNumber = registerRequest.phoneNumber
        };
    
        var result = await userRepository.Register(user, registerRequest.password, registerRequest.role);
        return Ok(result);
        
    }

    
    [HttpGet]
    [Route("/logout")]

public async Task<ActionResult> Logout()
{
    // Выполните логику выхода пользователя
    var result =  userRepository.Logout();
    return Ok(new { message = "Logged out successfully", result });
}

    
    [HttpDelete]
    [Route("/deleteUser")]

    public async Task<ActionResult> Delete(string email)
    {
        var result = await userRepository.Delete(email);
        return Ok(result);
    }
    
}