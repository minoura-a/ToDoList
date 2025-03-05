using TodoList.Models;

namespace TodoList.Sevices
{
    public class TodoAppState
    {
        public TodoItemGroup CurrentGroup { get; set; } = new TodoItemGroup();
    }
}
