namespace Domain;
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ToDo> ToDos { get; set; }
}