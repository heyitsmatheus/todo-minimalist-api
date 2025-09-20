using Microsoft.EntityFrameworkCore;
using Todo.Minimalist.Api.Models;

namespace Todo.Minimalist.Api.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
