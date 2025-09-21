using System.ComponentModel.DataAnnotations;

namespace Todo.Minimalist.Api.Entities;

public class TodoItem
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Title { get; set; } = string.Empty;
    public bool IsDone { get; set; }
}