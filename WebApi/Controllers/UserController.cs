using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;
[Route("[controller]")]
public class UserController:ControllerBase
{
    private readonly IUserService _service;
    public UserController(IUserService service)=>_service = service;


    [HttpGet("GetUsersAsync")]
    public async Task<Response<List<GetUserDto>>> GetUsersAsync()=>await _service.GetUsersAsync();

    [HttpGet("GetUserByIdAsync")]
    public async Task<Response<GetUserDto>> GetUserByIdAsync(int id)=>await _service.GetUserByIdAsync(id);

    [HttpPost("AddUserAsync")]
    public async Task<Response<BaseUserDto>> AddUserAsync(AddUserDto model)=>await _service.AddUserAsync(model);
    
    [HttpPut("UpdateUserAsync")]
    public async Task<Response<BaseUserDto>> UpdateUserAsync(AddUserDto model)=>await _service.UpdateUserAsync(model);

    [HttpDelete("DeleteUserAsync")]
    public async Task<Response<string>> DeleteUserAsync(int id)=>await _service.DeleteUserAsync(id);
}
