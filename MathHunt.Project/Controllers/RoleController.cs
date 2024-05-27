using System.Net;
using MathHunt.Contracts.Identity;
using MathHunt.Core.Abstraction.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathHunt.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController(IRoleUserService userService) : ControllerBase
{
  [Authorize(Roles = "admin")]
  [HttpGet("/getRoles")]
  public async Task<IActionResult> GetAllRoles()
  {
    var roleList = await userService.GetRoles();
    return Ok(roleList);
  }
  
  [Authorize(Roles = "admin")]
  [HttpGet("/getUserRole")]
  public async Task<IActionResult> GetUserRole(string userEmail)
  {
    var userClaims = userService.GetUserRole(userEmail);
    return Ok(userClaims);
  }

  [Authorize(Roles = "admin")]
  [HttpPost("addRoles")]
  public async Task<IActionResult> AddRoles(string[] roles)
  {
    var userRoles = await userService.AddRole(roles);
    if (userRoles.Count == 0)
    {
      return BadRequest();
    }

    return Ok(userRoles);
  }
  
  [Authorize(Roles = "admin")]
  [HttpPost("/addRoleToUser")]
  public async Task<IActionResult> AddRoleToUser([FromBody] AddUserRoleRequest request)
  {
    var result = await userService.AddRoleToUser(request.email, request.roles);
    if (!result)
    {
      return BadRequest();
    }
    return StatusCode((int)HttpStatusCode.Created, result);
  }
}