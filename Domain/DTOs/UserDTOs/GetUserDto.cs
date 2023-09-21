namespace Domain;
public class GetUserDto:BaseUserDto
{
    public List<GetToDoDto> ToDos { get; set; } = new List<GetToDoDto>();
}
