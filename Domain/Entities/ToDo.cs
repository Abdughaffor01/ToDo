using Domain.Entities;
using System.ComponentModel.DataAnnotations;
namespace Domain;
public class ToDo
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int? CategoryId { get; set; }
    [MaxLength(30)]
    public string  Title { get; set; }
    [MaxLength(500)]
    public string Description { get; set; }
    public DateTime? DeadLine { get; set; }
    public DateTime? CreateAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    public User User { get; set; }
    public Category Category { get; set; }
    public List<Comment> Comments { get; set; }
}