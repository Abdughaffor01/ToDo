using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Infrastructure;
public class UserService : IUserService
{
    private readonly DataContext _context;
    public UserService(DataContext contex)=>_context = contex;

    public async Task<Response<BaseUserDto>> AddUserAsync(AddUserDto model)
    {
        try
        {
            var user = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                Mobile = model.Mobile,
                Password = model.Password
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return new Response<BaseUserDto>(new BaseUserDto()
            {
                Id=user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Mobile = user.Mobile,
                Password = user.Password
            });
        }
        catch (Exception ex)
        {
            return new Response<BaseUserDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> AssignTaskToUser(TaskUserDto model)
    {
        try
        {
            var user = await _context.Users.FindAsync(model.UserId);
            var toDo = await _context.ToDos.FindAsync(model.TodoId);
            if (user == null || toDo == null) return new Response<string>(HttpStatusCode.BadRequest);
            toDo.UserId= user.Id;
            await _context.SaveChangesAsync();
            return new Response<string>("Added task to user");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError,ex.Message);
        }
    }

    public async Task<Response<string>> DeleteTaskFromUser(TaskUserDto model)
    {
        try
        {
            var user = await _context.Users.FindAsync(model.UserId);
            var toDo = await _context.ToDos.FindAsync(model.TodoId);
            if (user == null || toDo == null) return new Response<string>(HttpStatusCode.BadRequest);
            toDo.UserId = 0;
            await _context.SaveChangesAsync();
            return new Response<string>("Deleted task from user");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> DeleteUserAsync(int id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return new Response<string>(HttpStatusCode.BadRequest);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return new Response<string>("Successfuly deleted user");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError,ex.Message);
        }
    }

    public async Task<Response<GetUserDto>> GetUserByIdAsync(int id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return new Response<GetUserDto>(HttpStatusCode.BadRequest);
            var getUser = new GetUserDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Mobile = user.Mobile,
                Password = user.Password,
                ToDos = user.ToDos.Select(t => new GetToDoDto()
                {
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
            };
            return new Response<GetUserDto>(getUser);
        }
        catch (Exception ex)
        {
            return new Response<GetUserDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<List<GetUserDto>>> GetUsersAsync()
    {
        try
        {
            var users = await _context.Users.Select(user => new GetUserDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Mobile = user.Mobile,
                Password = user.Password,
                ToDos = user.ToDos.Select(t => new GetToDoDto()
                {
                    UserId = t.Id,
                    CategoryName=t.Category.Name,
                    Title = t.Title,
                    Description = t.Description,
                    CreateAt = t.CreateAt,
                    DeadLine = t.DeadLine,
                    UpdateAt = t.UpdateAt,
                    Comments =t.Comments.Select(c => new GetCommentDto()
                    {
                        Id=c.Id, 
                        CommentText=c.CommentText, 
                        ToDoId=c.ToDoId,
                    }).ToList()
                }).ToList()
            }).ToListAsync();
            return new Response<List<GetUserDto>>(users);
        }
        catch (Exception ex)
        {
            return new Response<List<GetUserDto>>(HttpStatusCode.InternalServerError,ex.Message);
        }
    }

    public async Task<Response<BaseUserDto>> UpdateUserAsync(AddUserDto model)
    {
        try
        {
            var user = await _context.Users.FindAsync(model.Id);
            if (user == null) return new Response<BaseUserDto>(HttpStatusCode.BadRequest);
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.Mobile = model.Mobile;
            user.Password = model.Password;
            await _context.SaveChangesAsync();
            return new Response<BaseUserDto>(new BaseUserDto() {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Mobile = user.Mobile,
                Password = user.Password
            });
        }
        catch (Exception ex)
        {
            return new Response<BaseUserDto>(HttpStatusCode.InternalServerError,ex.Message);
        }
    }
}
