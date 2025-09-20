using Todo.Minimalist.Api.Models;

namespace Todo.Minimalist.Api.Tests;

public class TodoTests
{
    [Fact]
    public void CanCreateTodoItem()
    {
        var todo = new TodoItem(Guid.NewGuid(), "Estudar .NET 10", false);
        Assert.Equal("Estudar .NET 10", todo.Title);
        Assert.False(todo.IsDone);
    }
}
