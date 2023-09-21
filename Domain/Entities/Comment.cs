using System.ComponentModel.DataAnnotations;
namespace Domain;
public class Comment
{
    public int Id { get; set; }
    [MaxLength(500)]
    public string CommentText { get; set; }
    public int ToDoId { get; set; }
    public ToDo ToDo { get; set; }
}
