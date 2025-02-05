using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.Data;

public class TodoListContext : DbContext
{
    public string DbPath { get; }

    public TodoListContext(DbContextOptions<TodoListContext> options) : base(options)
    {
        DbPath = System.IO.Path.Combine(AppContext.BaseDirectory, "todolist.db");
    }

    public DbSet<TodoItem> TodoItems { get; set; }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}
