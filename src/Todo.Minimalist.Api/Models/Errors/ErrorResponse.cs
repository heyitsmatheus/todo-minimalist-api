namespace Todo.Minimalist.Api.Models.Errors;

public class ErrorResponse
{
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public List<FieldError>? Errors { get; set; }
}

public class FieldError
{
    public string Field { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;
}
