namespace TodoList.Models;

/// <summary>
/// Todo要素のステータスを表す列挙型
/// </summary>
public class TodoItem
{
    /// <summary>
    /// ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// タイトル
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 完了したかどうか
    /// </summary>
    public bool IsDone { get; set; }

    /// <summary>
    /// ステータス
    /// </summary>
    public TodoStatus Status { get; set; }
}