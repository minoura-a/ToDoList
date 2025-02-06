using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.Data;

public class TodoListContext : DbContext
{
    public TodoListContext(DbContextOptions<TodoListContext> options) : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems { get; set; }
}
