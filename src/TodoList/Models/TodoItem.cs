namespace TodoList.Models;

/// <summary>
/// Todo要素のステータスを表す列挙型
/// </summary>
public class TodoItem
{
    /// <summary>
    /// ID
    /// </summary>
    public string Guid { get; } = System.Guid.NewGuid().ToString();

    /// <summary>
    /// タイトル
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// ステータス
    /// </summary>
    public TodoStatus Status { get; set; }

    /// <summary>
    /// 完了したかどうか
    /// </summary>
    public bool IsDone { get; set; }

    /// <summary>
    /// 所属するグループの一覧
    /// </summary>
    public TodoItemGroup TodoItemGroup { get; set; } = new TodoItemGroup();

    public TodoItem() { }

    public TodoItem(TodoItemGroup todoItemGroup)
    {
        TodoItemGroup = todoItemGroup;
    }
}