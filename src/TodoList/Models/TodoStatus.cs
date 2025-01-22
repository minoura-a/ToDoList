namespace TodoList.Models;

/// <summary>
/// Todo要素のステータスを表す列挙型
/// </summary>
public enum TodoStatus
{
    /// <summary>
    /// 未完了
    /// </summary>
    Incomplete,

    /// <summary>
    /// 進行中
    /// </summary>
    Doing,

    /// <summary>
    /// 完了
    /// </summary>
    Complete

}