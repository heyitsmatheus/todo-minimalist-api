namespace Todo.Minimalist.Api.DTOs
{
    public record TodoItemReadDto(Guid Id, string Title, bool IsDone);
}
