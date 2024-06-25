using MathHunt.Contracts.Role;
using MathHunt.Core.Abstraction.IServices;
using Microsoft.AspNetCore.Mvc;

namespace MathHunt.Controllers;

[ApiController]
public class PhotoUserController(IPhotoUserService service) : ControllerBase
{
    [HttpPost("/createUserPhoto")]
    public async Task<ActionResult<string>> CreateUserPhoto([FromForm] UpsertPhotoRequest request)
    {
        var result = await service.CreatePhoto(request.path, request.appUserId);
        return Ok(new { message = $"Create user photo successfully {result}" });
    }
    
    [HttpPut("/updateUserPhoto")]
    public async Task<ActionResult<string>> UpdateUserPhoto([FromForm] UpsertPhotoRequest request)
    {
        var result = await service.UpdatePhoto(request.path, request.appUserId);
        return Ok(new { message = $"Update user photo successfully {result}" });
    }
    
    [HttpDelete("/deleteUserPhoto")]
    public async Task<ActionResult<string>> DeleteUserPhoto(string appUserId)
    {
        var result = await service.DeletePhoto(appUserId);
        return Ok(new { message = $"Delete user photo successfully {result}" });
    }
}