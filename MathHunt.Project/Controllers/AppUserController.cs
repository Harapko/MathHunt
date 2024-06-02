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
        List<AllUserResponse> response = [];

        foreach (var user in userList)
        {
            var userSkillList = user.UserSkillsEntities.Select(u => u.SkillName);
            var userResponse = new AllUserResponse(user.UserName, user.UserSurname, user.Email, user.PhoneNumber, user.Role,
                userSkillList.ToArray());
            response.Add(userResponse);
        }

        return Ok(response);
    }

    [HttpPost]
    [Route("/getUserByName")]
    public async Task<ActionResult> GetUserByName([FromBody] UserByEmailRequest request)
    {
        if (request.name.Length >= 3)
        {
            var user = await userRepository.GetByName(request.name);
            var result = new UserByEmailResponse(user.UserName, user.UserSurname, user.Email, user.PhoneNumber, user.Role);
            return Ok(result);
        }
        else
        {
            return BadRequest(new { message = "Email is null!" });
        }
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
        return Ok(new { message = "User registered successfully!" });
        
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