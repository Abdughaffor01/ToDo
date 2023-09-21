namespace Domain;
public class GetToDoDto:BaseToDoDto
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public DateTime? CreateAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    public List<GetCommentDto> Comments { get; set; } = new List<GetCommentDto>();
}
