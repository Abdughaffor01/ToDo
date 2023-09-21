using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;
public class DataContext:DbContext
{
    public DataContext(DbContextOptions<DataContext> option) : base(option) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ToDo> ToDos { get; set; }
    public DbSet<Comment> Comments { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
        builder.Entity<User>().HasIndex(u => u.Email).IsUnique();
    }
}
