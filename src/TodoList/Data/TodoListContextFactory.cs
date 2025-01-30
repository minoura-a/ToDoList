using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TodoList.Data;

public class TodoListContextFactory: IDesignTimeDbContextFactory<TodoListContext>
{
    public TodoListContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TodoListContext>();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        optionsBuilder.UseSqlite(configuration.GetConnectionString("DefaultConnection"));

        return new TodoListContext(optionsBuilder.Options);
    }
}