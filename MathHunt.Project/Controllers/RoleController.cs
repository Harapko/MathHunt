using System.Net;
using MathHunt.Contracts.Identity;
using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathHunt.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController(IRoleUserService userRoleService) : ControllerBase
{
    // [Authorize(Roles = "admin")]
    [HttpGet("/getRoles")]
    public async Task<ActionResult<List<RoleModel>>> GetAllRoles()
    {
        var roleList = await userRoleService.GetRoles();
        return Ok(roleList);
    }

    // [Authorize(Roles = "admin")]
    [HttpGet("/getUserRole")]
    public async Task<ActionResult<List<string>>> GetUserRole(string userEmail)
    {
        var userClaims = await userRoleService.GetUserRole(userEmail);
        return Ok(userClaims);
    }

    // [Authorize(Roles = "admin")]
    [HttpPost("/addRoles")]
    public async Task<ActionResult<List<string>>> AddRoles(string roles)
    {
        var userRoles = await userRoleService.AddRole(roles);
        if (string.IsNullOrWhiteSpace(userRoles))
        {
            return BadRequest();
        }

        return Ok(userRoles);
    }

    // [Authorize(Roles = "admin")]
    [HttpPost("/addRoleToUser")]
    public async Task<ActionResult<bool>> AddRoleToUser([FromBody] AddUserRoleRequest request)
    {
        var result = await userRoleService.AddRoleToUser(request.email, request.roles);
        if (!result)
        {
            return BadRequest();
        }

        return StatusCode((int)HttpStatusCode.Created, result);
    }

    // [Authorize(Roles = "admin")]
    [HttpDelete("/deleteRoles")]
    public async Task<ActionResult> DeleteRoles(string roles)
    {
        await userRoleService.DeleteRole(roles);

        return Ok();
    }
}