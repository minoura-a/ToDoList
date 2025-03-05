using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using TodoList.Data;
using TodoList.Models;

namespace TodoList.Sevices
{
    public class DataBaseService
    {
        private readonly IDbContextFactory<TodoListContext> m_ContextFactory;

        public DataBaseService(IDbContextFactory<TodoListContext> contextFactory)
        {
            m_ContextFactory = contextFactory;
        }

        #region TodoItemGroup関連のメソッド

        /// <summary>
        /// TodoItemGroupを作成する
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public async Task<TodoItemGroup> CreateTodoItemGroupAsync(TodoItemGroup group)
        {
            using var context = m_ContextFactory.CreateDbContext();
            context.TodoItemGroups.Add(group);
            await context.SaveChangesAsync();
            return group;
        }

        /// <summary>
        /// TodoItemGroupをすべて取得する
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public async Task<IList<TodoItemGroup>> GetAllTodoItemGroupsAsync()
        {
            using var context = m_ContextFactory.CreateDbContext();
            return await context.TodoItemGroups.ToListAsync();
        }

        /// <summary>
        /// TodoItemGroupをId指定で取得する
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public async Task<TodoItemGroup?> GetTodoItemGroupAsync(string guid)
        {
            using var context = m_ContextFactory.CreateDbContext();
            return await context.TodoItemGroups
                                .FirstOrDefaultAsync(c => c.Guid == guid);
        }

        /// <summary>
        /// TodoItemGroupを名前指定で取得する
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<TodoItemGroup?> GetTodoItemGroupByNameAsync(string name)
        {
            using var context = m_ContextFactory.CreateDbContext();
            return await context.TodoItemGroups
                                .FirstOrDefaultAsync(g => g.Name == name);
        }

        /// <summary>
        /// TodoItemGroupを更新する
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public async Task UpdateTodoItemGroupAsync(TodoItemGroup group)
        {
            using var context = m_ContextFactory.CreateDbContext();
            var updateGroup = await context.TodoItemGroups
                                          .FirstOrDefaultAsync(c => c.Guid == group.Guid);
            if (updateGroup != null)
            {
                updateGroup.Name = group.Name;
                context.TodoItemGroups.Update(updateGroup);
            }

            context.TodoItemGroups.Update(group);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// TodoItemGroupを削除する
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public async Task RemoveTodoItemGroupAsync(TodoItemGroup group)
        {
            using var context = m_ContextFactory.CreateDbContext();
            context.TodoItemGroups.Remove(group);
            var items = group.Items;
            await context.SaveChangesAsync();
        }

        #endregion

        #region TodoItem関連のメソッド

        /// <summary>
        /// TodoItemを取得する
        /// </summary>
        /// <returns></returns>
        public async Task<List<TodoItem>> GetTodoItemsAsync()
        {
            using var context = m_ContextFactory.CreateDbContext();
            var groups = await context.TodoItemGroups.Include(g => g.Items).ToListAsync();
            return groups.SelectMany(g => g.Items).ToList();
        }

        /// <summary>
        /// TodoItemをGuid指定で取得する
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public async Task<TodoItem?> GetTodoItemAsync(string guid)
        {
            using var context = m_ContextFactory.CreateDbContext();
            var group = await context.TodoItemGroups.Include(g => g.Items)
                                                    .FirstOrDefaultAsync(g => g.Items.Any(i => i.Guid == guid));
            return group?.Items.FirstOrDefault(i => i.Guid == guid);
        }

        /// <summary>
        /// TodoItemを作成する
        /// </summary>
        /// <param name="group"></param>
        /// <param name="todoItem"></param>
        /// <returns></returns>
        public async Task<TodoItem> CreateTodoItemAsync(TodoItemGroup group, TodoItem todoItem)
        {
            using var context = m_ContextFactory.CreateDbContext();
            var existingGroup = await context.TodoItemGroups.Include(g => g.Items)
                                                            .FirstOrDefaultAsync(g => g.Guid == group.Guid);
            if (existingGroup != null)
            {
                existingGroup.Items.Add(todoItem);
                context.TodoItemGroups.Update(existingGroup);
                await context.SaveChangesAsync();
            }

            return todoItem;
        }

        /// <summary>
        /// TodoItemを更新する
        /// </summary>
        /// <param name="collectionGuid"></param>
        /// <param name="todoItem"></param>
        /// <returns></returns>
        public async Task UpdateTodoItemAsync(string collectionGuid, TodoItem todoItem)
        {
            using var context = m_ContextFactory.CreateDbContext();
            var group = await context.TodoItemGroups.Include(g => g.Items)
                                                    .FirstOrDefaultAsync(g => g.Guid == collectionGuid);
            if (group != null)
            {
                var updateItem = group.Items.FirstOrDefault(i => i.Guid == todoItem.Guid);
                if (updateItem != null)
                {
                    updateItem.Title = todoItem.Title;
                    updateItem.Status = todoItem.Status;
                    updateItem.TodoItemGroup = todoItem.TodoItemGroup;
                    context.TodoItemGroups.Update(group);
                    await context.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// TodoItemを削除する
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task DeleteTodoItemAsync(TodoItem item)
        {
            using var context = m_ContextFactory.CreateDbContext();
            var group = await context.TodoItemGroups.Include(g => g.Items)
                                                    .FirstOrDefaultAsync(g => g.Guid == item.TodoItemGroup.Guid);
            if (group != null)
            {
                var deleteItem = group.Items.FirstOrDefault(i => i.Guid == item.Guid);
                if (deleteItem != null)
                {
                    group.Items.Remove(deleteItem);
                    context.TodoItemGroups.Update(group);
                    await context.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// 完了もしくは未完了のTodoItemを取得する
        /// </summary>
        /// <param name="isDone"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TodoItem>> GetTodoItemsByIsDoneAsync(TodoItemGroup itemGroup, bool isDone)
        {
            using var context = m_ContextFactory.CreateDbContext();
            var group = await context.TodoItemGroups.Include(g => g.Items)
                                                    .FirstOrDefaultAsync(g => g.Items.Any(i => i.TodoItemGroup.Guid == itemGroup.Guid));
            return group?.Items.Where(e => e.IsDone == isDone);
        }

        # endregion
    }
}
