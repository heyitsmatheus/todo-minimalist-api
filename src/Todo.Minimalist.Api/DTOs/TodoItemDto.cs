using System.ComponentModel.DataAnnotations;

namespace Todo.Minimalist.Api.DTOs;

public class TodoItemDto
{
    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
    public string Title { get; set; } = string.Empty;

    public bool IsDone { get; set; }
}