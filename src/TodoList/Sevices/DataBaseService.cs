using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Sevices;

public class DataBaseService
{
    private readonly TodoListContextFactory m_ContextFactory;

    public DataBaseService(TodoListContextFactory contextFactory)
    {
        m_ContextFactory = contextFactory;
    }

    public async Task<List<TodoItem>> GetTodoItemsAsync()
    {
        await using var context = m_ContextFactory.CreateDbContext(new string[0]);
        return await context.TodoItems.ToListAsync();
    }

    public async Task<TodoItem> CreateTodoItemAsync(TodoItem todoItem)
    {
        await using var context = m_ContextFactory.CreateDbContext(new string[0]);
        context.TodoItems.Add(todoItem);
        await context.SaveChangesAsync();
        return todoItem;
    }

    public async Task<TodoItem> GetTodoItemAsync(int id)
    {
        await using var context = m_ContextFactory.CreateDbContext(new string[0]);
        return await context.TodoItems.FindAsync(id);
    }

    public async Task UpdateTodoItemAsync(int id, TodoItem todoItem)
    {
        await using var context = m_ContextFactory.CreateDbContext(new string[0]);
        context.TodoItems.Update(todoItem);
        await context.SaveChangesAsync();
    }

    public async Task DeleteTodoItemAsync(int id)
    {
        await using var context = m_ContextFactory.CreateDbContext(new string[0]);
        var todoItem = await context.TodoItems.FindAsync(id);
        context.TodoItems.Remove(todoItem);
        await context.SaveChangesAsync();
    }

    public async Task<bool> TodoItemExistsAsync(int id)
    {
        await using var context = m_ContextFactory.CreateDbContext(new string[0]);
        return await context.TodoItems.AnyAsync(e => e.Id == id);
    }

    public async Task<List<TodoItem>> GetTodoItemsByStatusAsync(TodoStatus status)
    {
        await using var context = m_ContextFactory.CreateDbContext(new string[0]);
        return await context.TodoItems.Where(e => e.Status == status).ToListAsync();
    }
}