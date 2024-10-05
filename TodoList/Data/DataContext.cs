using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }
        // Register ToDoItem class to the DataContext
    public DbSet<TodoItem> TodoItems { get; set; } = null!;
}