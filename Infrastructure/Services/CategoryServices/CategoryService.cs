using Domain;
using Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Infrastructure;
public class CategoryService : ICategoryService
{
    private readonly DataContext _context;
    public CategoryService(DataContext context)=>_context = context;

    public async Task<Response<BaseCategoryDto>> AddCategoryAsync(AddCategoryDto model)
    {
        try
        {
            var category = new Category()
            {
                Name = model.Name,
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return new Response<BaseCategoryDto>(new BaseCategoryDto() { 
                Id=category.Id, Name = category.Name,
            });
        }
        catch (Exception ex)
        {
            return new Response<BaseCategoryDto>(HttpStatusCode.InternalServerError,ex.Message);
        }
    }

    public async Task<Response<string>> DeleteCategoryAsync(int id)
    {
        try
        {
            var category= await _context.Categories.FindAsync(id);
            if (category == null) return new Response<string>(HttpStatusCode.BadRequest);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return new Response<string>("Successfuly deleted category");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError,ex.Message);
        }
    }

    public async Task<Response<GetAllToDo>> GetCategoriesAsync()
    {
        try
        {
            var categories = await _context.Categories.Select(c => new GetCategoryDto()
            {
                Id = c.Id,
                Name = c.Name,
                ToDos = c.ToDos.Select(t => new GetToDoDto()
                {
                    Id=t.Id,
                    UserId = t.Id,
                    CategoryName = t.Category.Name,
                    Title = t.Title,
                    Description = t.Description,
                    CreateAt = t.CreateAt,
                    DeadLine = t.DeadLine,
                    UpdateAt = t.UpdateAt,
                    Comments = t.Comments.Select(c => new GetCommentDto()
                    {
                        Id = c.Id,
                        CommentText = c.CommentText,
                        ToDoId = c.ToDoId,
                    }).ToList()
                }).ToList()
            }).ToListAsync();
            var nocategory = await _context.ToDos.Where(x=>x.CategoryId==null).Select(t => new GetToDoDto() 
            {
                Id = t.Id,
                UserId = t.Id,
                CategoryName = t.Category.Name,
                Title = t.Title,
                Description = t.Description,
                CreateAt = t.CreateAt,
                DeadLine = t.DeadLine,
                UpdateAt = t.UpdateAt,
                Comments = t.Comments.Select(c => new GetCommentDto()
                {
                    Id = c.Id,
                    CommentText = c.CommentText,
                    ToDoId = c.ToDoId
                }).ToList()
            }).ToListAsync();
            var allquote = new GetAllToDo
            {
                Categories = categories,
                QuoteNoCategory = nocategory
            };
            return new Response<GetAllToDo>(allquote); 
        }
        catch (Exception ex)
        {
            return new Response<GetAllToDo>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<GetCategoryDto>> GetCategoryByIdAsync(int id)
    {
        try
        {
            var category=await _context.Categories.FindAsync(id);
            if (category == null) return new Response<GetCategoryDto>(HttpStatusCode.BadRequest);
            return new Response<GetCategoryDto>(new GetCategoryDto() { 
                Id=category.Id,
                Name=category.Name,
                ToDos=category.ToDos.Select(c=>new GetToDoDto() {
                    UserId=c.Id,
                    CategoryName=c.Category.Name,
                    Title=c.Title,
                    Description=c.Description,
                    Comments=c.Comments.Select(t=>new GetCommentDto() { 
                        Id = t.Id,
                        ToDoId=t.ToDoId,
                        CommentText = t.CommentText,
                    }).ToList(),
                }).ToList()
            });
        }
        catch (Exception ex)
        {
            return new Response<GetCategoryDto>(HttpStatusCode.InternalServerError,ex.Message);
        }
    }

    public async Task<Response<BaseCategoryDto>> UpdateCategoryAsync(AddCategoryDto model)
    {
        try
        {
            var category = await _context.Categories.FindAsync(model.Id);
            if (category == null) return new Response<BaseCategoryDto>(HttpStatusCode.BadRequest);
            category.Name = model.Name;
            await _context.SaveChangesAsync();
            return new Response<BaseCategoryDto>(new BaseCategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
            });
        }
        catch (Exception ex)
        {
            return new Response<BaseCategoryDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
