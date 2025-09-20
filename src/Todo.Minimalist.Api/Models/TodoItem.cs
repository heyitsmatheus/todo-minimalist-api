namespace Todo.Minimalist.Api.Models;

public record TodoItem(Guid Id, string Title, bool IsDone);