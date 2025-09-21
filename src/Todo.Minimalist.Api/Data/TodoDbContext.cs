using Microsoft.EntityFrameworkCore;
using Todo.Minimalist.Api.Entities;

namespace Todo.Minimalist.Api.Data
{
    public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options)
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoItem>().HasData(
                new TodoItem
                {
                    Id = Guid.Parse("a1e1c1d1-0000-0000-0000-000000000001"),
                    Title = "Aprender .NET 10",
                    IsDone = false
                },
                new TodoItem
                {
                    Id = Guid.Parse("a1e1c1d1-0000-0000-0000-000000000002"),
                    Title = "Criar API Minimalista",
                    IsDone = true
                },
                new TodoItem
                {
                    Id = Guid.Parse("a1e1c1d1-0000-0000-0000-000000000003"),
                    Title = "Testar Seed Data",
                    IsDone = false
                }
            );
        }
    }
}
