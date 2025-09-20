using System.ComponentModel.DataAnnotations;

namespace Todo.Minimalist.Api.Models;

public class TodoItem
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string Title { get; set; }
    public bool IsDone { get; set; }
}