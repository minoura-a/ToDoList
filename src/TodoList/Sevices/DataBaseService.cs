using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Sevices;

public class DataBaseService
{
    private readonly IDbContextFactory<TodoListContext> m_ContextFactory;

    public DataBaseService(IDbContextFactory<TodoListContext> contextFactory)
    {
        m_ContextFactory = contextFactory;
    }

    public async Task<List<TodoItem>> GetTodoItemsAsync()
    {
        using var context = m_ContextFactory.CreateDbContext();
        return await context.TodoItems.ToListAsync();
    }

    public async Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem)
    {
        using var context = m_ContextFactory.CreateDbContext();
        context.TodoItems.Add(todoItem);
        await context.SaveChangesAsync();
        return todoItem;
    }

    public async Task<TodoItem?> GetTodoItemAsync(int id)
    {
        using var context = m_ContextFactory.CreateDbContext();
        return await context.TodoItems.FindAsync(id);
    }

    public async Task UpdateTodoItemAsync(TodoItem todoItem)
    {
        using var context = m_ContextFactory.CreateDbContext();
        context.TodoItems.Update(todoItem);
        await context.SaveChangesAsync();
    }

    public async Task DeleteTodoItemAsync(int id)
    {
        using var context = m_ContextFactory.CreateDbContext();
        var todoItem = await context.TodoItems.FindAsync(id);
        if(todoItem == null) return;
        context.TodoItems.Remove(todoItem);
        await context.SaveChangesAsync();
    }

    public async Task<bool> TodoItemExistsAsync(int id)
    {
        using var context = m_ContextFactory.CreateDbContext();
        return await context.TodoItems.AnyAsync(e => e.Id == id);
    }

    public async Task<List<TodoItem>> GetTodoItemsByStatusAsync(TodoStatus status)
    {
        using var context = m_ContextFactory.CreateDbContext();
        return await context.TodoItems.Where(e => e.Status == status).ToListAsync();
    }
}