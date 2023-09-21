using System.ComponentModel.DataAnnotations;

namespace Domain;
public class BaseToDoDto
{
    public int UserId { get; set; }
    public int? CategoryId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? DeadLine { get; set; }
}
