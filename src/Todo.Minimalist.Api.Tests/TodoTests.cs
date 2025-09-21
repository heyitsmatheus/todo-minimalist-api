using Todo.Minimalist.Api.Entities;

namespace Todo.Minimalist.Api.Tests;

public class TodoTests
{
    [Fact]
    public void CanCreateTodoItem()
    {
        var todo = new TodoItem() { Id = Guid.NewGuid(), Title = "Estudar .NET 10", IsDone = false };
        Assert.Equal("Estudar .NET 10", todo.Title);
        Assert.False(todo.IsDone);
    }
}