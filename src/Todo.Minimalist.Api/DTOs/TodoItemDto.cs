using System.ComponentModel.DataAnnotations;

namespace Todo.Minimalist.Api.DTOs;

public class TodoItemDto
{
    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
    public required string Title { get; init; }

    public bool IsDone { get; init; }
}