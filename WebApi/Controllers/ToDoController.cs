using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;
[Route("[controller]")]
public class ToDoController:ControllerBase
{
    private readonly IToDoService _service;
    public ToDoController(IToDoService service)=>_service = service;


    [HttpGet("GetToDosAsync")]
    public async Task<Response<List<GetToDoDto>>> GetToDosAsync()=>await _service.GetToDosAsync();

    [HttpGet("GetToDoByIdAsync")]
    public async Task<Response<GetToDoDto>> GetToDoByIdAsync(int id)=>await _service.GetToDoByIdAsync(id);

    [HttpPost("AddToDoByIdAsync")]
    public async Task<Response<BaseToDoDto>> AddToDoAsync([FromBody]AddToDoDto model)=>await _service.AddToDoAsync(model);

    [HttpPut("UpdateToDoByIdAsync")]
    public async Task<Response<BaseToDoDto>> UpdateToDoByIdAsync(UpdateToDoDto model) => await _service.UpdateToDoByIdAsync(model);

    [HttpDelete("DeleteToDoByIdAsync")]
    public async Task<Response<string>> DeleteToDoByIdAsync(int id)=>await _service.DeleteToDoByIdAsync(id);


}
