using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.Data
{
    public class TodoListContext : DbContext
    {
        public TodoListContext(DbContextOptions<TodoListContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=TodoList.db");
        }

        /// <summary>
        /// Todo要素のグループの一覧
        /// </summary>
        public DbSet<TodoItemGroup> TodoItemGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 主キーの設定
            modelBuilder.Entity<TodoItemGroup>()
               .HasKey(g => g.Guid);

            modelBuilder.Entity<TodoItem>()
                .HasKey(i => i.Guid);
        }
    }
}
