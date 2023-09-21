namespace Domain;
public class UpdateToDoDto:BaseToDoDto
{
    public int Id { get; set; }
    public DateTime UpdateAt { get; set; }
}
