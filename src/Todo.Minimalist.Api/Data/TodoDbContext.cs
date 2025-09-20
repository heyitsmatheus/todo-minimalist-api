using Microsoft.EntityFrameworkCore;
using Todo.Minimalist.Api.Models;

namespace Todo.Minimalist.Api.Data
{
    public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options)
    {
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
