using System.ComponentModel.DataAnnotations;
namespace Domain.Entities;
public class User
{
    public int Id { get; set; }
    [MaxLength(30)]
    public string UserName { get; set; }
    [MaxLength(100)]    
    public string Email { get; set;}
    [MaxLength(13)]
    public string Mobile { get; set;}
    [MaxLength(50)]
    public string Password { get; set;}
    public List<ToDo> ToDos { get; set; }
}
