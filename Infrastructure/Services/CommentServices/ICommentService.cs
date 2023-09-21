using Domain;
namespace Infrastructure;
public interface ICommentService
{
    Task<Response<List<GetCommentDto>>> GetCommentsAsync();
    Task<Response<GetCommentDto>> GetCommentByIdAsync(int id);
    Task<Response<BaseCommentDto>> AddCommentAsync(AddCommentDto model);
    Task<Response<BaseCommentDto>> UpdateCommentAsync(AddCommentDto model);
    Task<Response<string>> DeleteCommentAsync(int id);
}
