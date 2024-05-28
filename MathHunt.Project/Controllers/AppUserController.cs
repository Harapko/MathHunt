using MathHunt.Contracts.Identity;
using MathHunt.Core.Abstraction.IRepositories;
using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;
using MathHunt.DataAccess;
using MathHunt.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MathHunt.Controllers;

[ApiController]
[Route("[controller]")]
public class AppUserController(
    UserManager<AppUserEntity> userManager,
    IRoleUserService roleService,
    SignInManager<AppUserEntity> signInManager,
    IUserSkillService skillService,
    AppDbContext context) : ControllerBase
{
    
    [HttpGet("/getUser")]
    public async Task<ActionResult> GetUser()
    {
        var userList = await userManager.Users.ToListAsync();
        return Ok(userList);
    }
    
    [HttpGet("/getUserBySkill")]
    public async Task<ActionResult> GetUserBySkill(string skillName)
    {
        var result = await context.UserSkill
            .Where(s => s.SkillName == skillName)
            .Include(s => s.AppUserEntities)
            .ToListAsync();
        
        return Ok(result);
    }
    
    [HttpGet("/getSkillByUser")]
    public async Task<ActionResult> GetSkillByUser(string email)
    {
        var result = await context.AppUsers
            .Where(u => u.Email == email)
            .Include(u => u.UserSkillsEntities)
            .ToListAsync();
        
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
    
        var result = await userManager.CreateAsync(user, registerRequest.password);
        await roleService.AddRoleToUser(registerRequest.email, registerRequest.role);
        return Ok(result);
        
    }

    [HttpPost("/login")]
    public async Task<ActionResult> Login([FromBody] LoginCustomRequest loginRequest)
    {
        var result = await signInManager.PasswordSignInAsync(loginRequest.email, loginRequest.password,
            loginRequest.rememberMe, false);
        return Ok(result);
    }

    [HttpDelete("/deleteUser")]
    public async Task<ActionResult> Delete(string emailId)
    {
        var userEntity = await userManager.FindByEmailAsync(emailId);
        var result = await userManager.DeleteAsync(userEntity);
        return Ok(result);
    }

    

    
}