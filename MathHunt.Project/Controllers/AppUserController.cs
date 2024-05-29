using MathHunt.Contracts.Identity;
using MathHunt.DataAccess;
using MathHunt.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MathHunt.Controllers;

[ApiController]
[Route("[controller]")]
public class AppUserController(IAppUserRepository userRepository) : ControllerBase
{
    [HttpGet("/getUser")]
    public async Task<ActionResult> GetUser()
    {
        var userList = await userRepository.Get();
        return Ok(userList);
    }
    
    
    [HttpGet("/getSkillsUser")]
    public async Task<ActionResult> GetSkillByUser(string email)
    {
        var result = await userRepository.GetSkillsUser(email);
        
        return Ok(result);
    }
    
    
    [HttpPost("/register")]
    public async Task<ActionResult> Register([FromBody] RegisterCustomRequest registerRequest)
    {
        AppUserEntity user = new()
        {
            UserName = registerRequest.userName,
            UserSurname = registerRequest.userSurname,
            Email = registerRequest.email,
            PhoneNumber = registerRequest.phoneNumber
        };
    
        var result = await userRepository.Register(user, registerRequest.password, registerRequest.role);
        return Ok(result);
        
    }

    
    [HttpPost("/login")]
    public async Task<ActionResult> Login([FromBody] LoginCustomRequest loginRequest)
    {
        var result = await userRepository.Login(loginRequest.userName, loginRequest.password,
            loginRequest.rememberMe);
        return Ok(result);
    }
    
    [HttpPost("/logout")]
    public async Task<ActionResult> Logout()
    {
        var result =  userRepository.Logout();
        return Ok(result);
    }

    
    [HttpDelete("/deleteUser")]
    public async Task<ActionResult> Delete(string email)
    {
        var result = await userRepository.Delete(email);
        return Ok(result);
    }
    
}