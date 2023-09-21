using Domain;
using Microsoft.EntityFrameworkCore;    
using System.Net;
namespace Infrastructure;
public class ToDoService : IToDoService
{
    private readonly DataContext _context;
    public ToDoService(DataContext context)=>_context=context;
    public async Task<Response<BaseToDoDto>> AddToDoAsync(AddToDoDto model)
    {
        try
        {
            var toDo=new ToDo() { 
                UserId = model.UserId,
                Title = model.Title,
                Description = model.Description,
                CategoryId = model.CategoryId,
                DeadLine = model.DeadLine
            };
            await _context.ToDos.AddAsync(toDo);
            await _context.SaveChangesAsync();
            return new Response<BaseToDoDto>(new BaseToDoDto() { 
                UserId = toDo.UserId,
                Title = toDo.Title,
                Description= toDo.Description,
                CategoryId = toDo.CategoryId,
            });
        }
        catch (Exception ex)
        {
            return new Response<BaseToDoDto>(HttpStatusCode.InternalServerError,ex.Message);
        }
    }

    public async Task<Response<string>> DeleteToDoByIdAsync(int id)
    {
        try
        {
            var toDo = await _context.ToDos.FindAsync(id);
            if (toDo == null) return new Response<string>(HttpStatusCode.BadRequest);
            _context.ToDos.Remove(toDo);
            await _context.SaveChangesAsync();
            return new Response<string>("Successfuly deleted ToDo");
        }
        catch (Exception ex)
        {
            return new Response<string> ( HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<GetToDoDto>> GetToDoByIdAsync(int id)
    {
        try
        {
            var toDo = await _context.ToDos.FindAsync(id);
            if (toDo == null) return new Response<GetToDoDto>(HttpStatusCode.BadRequest);
            return new Response<GetToDoDto>(new GetToDoDto() { 
                Id=toDo.Id,
                UserId = toDo.UserId,
                Title = toDo.Title,
                Description = toDo.Description,
                CreateAt = toDo.CreateAt,
                DeadLine = toDo.DeadLine,
                CategoryName=toDo.Category.Name,
                UpdateAt = toDo.UpdateAt
            });
        }
        catch (Exception ex)
        {
            return new Response<GetToDoDto> (HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<List<GetToDoDto>>> GetToDosAsync()
    {
        try
        {
            var toDos = await _context.ToDos.Select(t => new GetToDoDto()
            {
                Id = t.Id,
                UserId = t.UserId,
                Title = t.Title,
                Description = t.Description,
                CreateAt = t.CreateAt,
                DeadLine = t.DeadLine,
                CategoryName = t.Category.Name,
                UpdateAt = t.UpdateAt
            }).ToListAsync();
            return new Response<List<GetToDoDto>>(toDos);
        }
        catch (Exception ex)
        {
            return new Response<List<GetToDoDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<BaseToDoDto>> UpdateToDoByIdAsync(UpdateToDoDto model)
    {
        try
        {
            var toDo = await _context.ToDos.FindAsync(model.Id);
            if (toDo == null) return new Response<BaseToDoDto>(HttpStatusCode.BadRequest);
            toDo.UserId = model.UserId;
            toDo.Title = model.Title;
            toDo.Description = model.Description;
            toDo.CategoryId = model.CategoryId;
            toDo.UpdateAt = DateTime.Now;
            toDo.DeadLine = DateTime.Now;
            await _context.SaveChangesAsync();
            return new Response<BaseToDoDto>(new BaseToDoDto() {
                UserId = toDo.UserId,
                Title = toDo.Title,
                Description = toDo.Description,
                CategoryId = toDo.CategoryId,
                DeadLine = toDo.DeadLine,
            });
        }
        catch (Exception ex)
        {
            return new Response<BaseToDoDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
