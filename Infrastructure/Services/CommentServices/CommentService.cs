using Domain;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Infrastructure;
public class CommentService : ICommentService
{
    private readonly DataContext _context;
    public CommentService(DataContext context)=>_context = context;
    public async Task<Response<BaseCommentDto>> AddCommentAsync(AddCommentDto model)
    {
        try
        {
            var comment = new Comment() { 
                ToDoId = model.ToDoId,
                CommentText = model.CommentText,
            };
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return new Response<BaseCommentDto>(new BaseCommentDto() { 
                Id=comment.Id,
                CommentText=comment.CommentText,
            });
        }
        catch (Exception ex)
        {
            return new Response<BaseCommentDto>(HttpStatusCode.InternalServerError,ex.Message);
        }
    }

    public async Task<Response<GetCommentDto>> GetCommentByIdAsync(int id)
    {
        try
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return new Response<GetCommentDto>(HttpStatusCode.BadRequest);
            return new Response<GetCommentDto>(new GetCommentDto() { 
                Id= comment.Id,
                CommentText= comment.CommentText,
            });
        }
        catch (Exception ex)
        {
            return new Response<GetCommentDto>(HttpStatusCode.InternalServerError,ex.Message);
        }
    }

    public async Task<Response<List<GetCommentDto>>> GetCommentsAsync()
    {
        try
        {
            var comments=await _context.Comments.Select(x => new GetCommentDto() { 
                Id = x.Id,
                CommentText= x.CommentText,
            }).ToListAsync();
            return new Response<List<GetCommentDto>>(comments);
        }
        catch (Exception ex)
        {
            return new Response<List<GetCommentDto>>(HttpStatusCode.InternalServerError,ex.Message);
        }
    }

    public async Task<Response<BaseCommentDto>> UpdateCommentAsync(AddCommentDto model)
    {
        try
        {
            var comment=await _context.Comments.FindAsync(model.Id);
            if (comment == null) return new Response<BaseCommentDto>(HttpStatusCode.BadRequest);
            comment.CommentText = model.CommentText;
            await _context.SaveChangesAsync();
            return new Response<BaseCommentDto>(new BaseCommentDto() { 
                Id=comment.Id,
                CommentText= comment.CommentText,
            });
        }
        catch (Exception ex)
        {
            return new Response<BaseCommentDto>(HttpStatusCode.InternalServerError,ex.Message);
        }
    }

    public async Task<Response<string>> DeleteCommentAsync(int id)
    {
        try
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return new Response<string>(HttpStatusCode.BadRequest);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return new Response<string>("Successfuly deleted comment");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError,ex.Message);
        }
    }
}
