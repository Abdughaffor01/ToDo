using Domain;

namespace Infrastructure;
public interface IToDoService
{
    Task<Response<List<GetToDoDto>>> GetToDosAsync();
    Task<Response<GetToDoDto>> GetToDoByIdAsync(int id);
    Task<Response<BaseToDoDto>> AddToDoAsync(AddToDoDto model);
    Task<Response<BaseToDoDto>> UpdateToDoByIdAsync(UpdateToDoDto model);
    Task<Response<string>> DeleteToDoByIdAsync(int id);
}
