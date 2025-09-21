using Todo.Minimalist.Api.DTOs;
using Todo.Minimalist.Api.Entities;
using Todo.Minimalist.Api.Extensions;
using Todo.Minimalist.Api.Models.Errors;

namespace Todo.Minimalist.Api.Tests;

public class TodoTests
{
    [Fact]
    public void TodoItemDto_ValidData_PassesValidation()
    {
        var dto = new TodoItemDto { Title = "Nova tarefa", IsDone = false };
        var result = dto.TryValidate(out var errors);
        Assert.True(result);
        Assert.Empty(errors);
    }

    [Fact]
    public void TodoItemDto_MissingTitle_FailsValidation()
    {
        var dto = new TodoItemDto { Title = "", IsDone = true };
        var result = dto.TryValidate(out var errors);
        Assert.False(result);
        Assert.Contains(errors, e => e.Field == "Title");
    }

    [Fact]
    public void TodoItemDto_TitleTooLong_FailsValidation()
    {
        var dto = new TodoItemDto { Title = new string('a', 101), IsDone = false };
        var result = dto.TryValidate(out var errors);
        Assert.False(result);
        Assert.Contains(errors, e => e.Field == "Title");
    }

    [Fact]
    public void TodoItem_Entity_CanBeCreated()
    {
        var todo = new TodoItem { Id = Guid.NewGuid(), Title = "Testar entidade", IsDone = true };
        Assert.Equal("Testar entidade", todo.Title);
        Assert.True(todo.IsDone);
    }

    [Fact]
    public void FieldError_CanBeCreated()
    {
        var error = new FieldError { Field = "Title", Error = "Obrigatório" };
        Assert.Equal("Title", error.Field);
        Assert.Equal("Obrigatório", error.Error);
    }
}