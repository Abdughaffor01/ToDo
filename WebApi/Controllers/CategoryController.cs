using Domain.DTOs;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;
[Route("[controller]")]

public class CategoryController:ControllerBase
{
    private readonly ICategoryService _service;
    public CategoryController(ICategoryService service)=>_service = service;


    [HttpGet("GetCategoriesAsync")]
    public async Task<Response<GetAllToDo>> GetCategoriesAsync()=>await _service.GetCategoriesAsync();

    [HttpGet("GetCategoryByIdAsync")]
    public async Task<Response<GetCategoryDto>> GetCategoryByIdAsync(int id)=>await _service.GetCategoryByIdAsync(id);

    [HttpPost("AddCategoryAsync")]
    public async Task<Response<BaseCategoryDto>> AddCategoryAsync(AddCategoryDto model)=>await _service.AddCategoryAsync(model);

    [HttpPut("UpdateCategoryAsync")]
    public async Task<Response<BaseCategoryDto>> UpdateCategoryAsync(AddCategoryDto model)=>await _service.UpdateCategoryAsync(model);

    [HttpDelete("DeleteCategoryAsync")]
    public async Task<Response<string>> DeleteCategoryAsync(int id)=>await _service.DeleteCategoryAsync(id);
}
