using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace TodoList.Models;

/// <summary>
/// Todo要素の一覧を表すクラス
/// </summary>
public class TodoItemGroup
{
    /// <summary>
    /// タイトル
    /// </summary>
    public string Name { get; set; } = "Todo List";

    /// <summary>
    /// guid
    /// </summary>
    public string Guid { get; } = System.Guid.NewGuid().ToString();

    /// <summary>
    /// Todo要素の一覧
    /// </summary>
    public List<TodoItem> Items { get; } = new List<TodoItem>();

    /// <summary>
    /// 要素の追加
    /// </summary>
    /// <param name="item"></param>
    public TodoItem Add(string title)
    {
        var item = new TodoItem(this)
        {
            Title = title,
            Status = TodoStatus.Incomplete
        };
        Items.Add(item);
        return item;
    }

    /// <summary>
    /// 要素の追加
    /// </summary>
    /// <param name="item"></param>
    public TodoItem Add(TodoItem item)
    {
        Items.Add(item);
        return item;
    }

    /// <summary>
    /// 要素の追加
    /// </summary>
    /// <param name="item"></param>
    public void Add(IEnumerable<TodoItem> todoItems)
    {
        foreach (var item in todoItems)
        {
            Items.Add(item);
        }
    }

    /// <summary>
    /// 要素の削除
    /// </summary>
    /// <param name="item"></param>
    public void Remove(TodoItem item)
    {
        Items.Remove(item);
    }

    /// <summary>
    /// 一覧のクリア
    /// </summary>
    public void Clear()
    {
        Items.Clear();
    }

    /// <summary>
    /// ステータスの変更
    /// </summary>
    /// <param name="item"></param>
    /// <param name="status"></param>
    public void ChangeStatus(TodoItem item, TodoStatus status)
    {
        item.Status = status;
    }

    /// <summary>
    /// タイトルの変更
    /// </summary>
    /// <param name="item"></param>
    /// <param name="title"></param>
    public void ChangeTitle(TodoItem item, string title)
    {
        item.Title = title;
    }

    /// <summary>
    /// IDから要素を取得
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public TodoItem? GetItem(string guid)
    {
        return Items.FirstOrDefault(i => i.Guid == guid);
    }

    /// <summary>
    /// ステータス空要素を取得
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public IEnumerable<TodoItem> GetItems(TodoStatus status)
    {
        return Items.Where(i => i.Status == status);
    }
}