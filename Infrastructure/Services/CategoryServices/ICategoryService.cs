using Domain;
using Domain.DTOs;
namespace Infrastructure;
public interface ICategoryService
{
    Task<Response<GetAllToDo>> GetCategoriesAsync();
    Task<Response<GetCategoryDto>> GetCategoryByIdAsync(int id);
    Task<Response<BaseCategoryDto>> AddCategoryAsync(AddCategoryDto model);
    Task<Response<BaseCategoryDto>> UpdateCategoryAsync(AddCategoryDto model);
    Task<Response<string>> DeleteCategoryAsync(int id);
}
