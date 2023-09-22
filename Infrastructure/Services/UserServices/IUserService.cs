using Domain;

namespace Infrastructure;
public interface IUserService
{
    Task<Response<List<GetUserDto>>> GetUsersAsync();
    Task<Response<GetUserDto>> GetUserByIdAsync(int id);
    Task<Response<BaseUserDto>> AddUserAsync(AddUserDto model);
    Task<Response<BaseUserDto>> UpdateUserAsync(AddUserDto model);
    Task<Response<string>> DeleteUserAsync(int id);
    Task<Response<string>> AssignTaskToUser(TaskUserDto model);
    Task<Response<string>> DeleteTaskFromUser(TaskUserDto model);
}
