using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;
[Route("[controller]")]

public class CommentController:ControllerBase
{
    private readonly ICommentService _service;
    public CommentController(ICommentService service)=>_service = service;


    [HttpGet("GetCommentsAsync")]
    public async Task<Response<List<GetCommentDto>>> GetCommentsAsync()=>await _service.GetCommentsAsync();

    [HttpGet("GetCommentByIdAsync")]
    public async Task<Response<GetCommentDto>> GetCommentByIdAsync(int id)=>await _service.GetCommentByIdAsync(id);

    [HttpPost("AddCommentAsync")]
    public async Task<Response<BaseCommentDto>> AddCommentAsync(AddCommentDto model)=>await _service.AddCommentAsync(model);

    [HttpPut("UpdateCommentAsync")]
    public async Task<Response<BaseCommentDto>> UpdateCommentAsync(AddCommentDto model)=>await _service.UpdateCommentAsync(model);

    [HttpDelete("DeleteCommentAsync")]
    public async Task<Response<string>> DeleteCommentAsync(int id)=>await _service.DeleteCommentAsync(id);  
}
