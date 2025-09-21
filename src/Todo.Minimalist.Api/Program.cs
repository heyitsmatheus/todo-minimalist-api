using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Minimalist.Api.Data;
using Todo.Minimalist.Api.DTOs;
using Todo.Minimalist.Api.Entities;
using Todo.Minimalist.Api.Extensions;
using Todo.Minimalist.Api.Middlewares;
using Todo.Minimalist.Api.Models.Errors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlite("Data Source=Data/todo.db"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddControllers();

var app = builder.Build();

app.UseGlobalExceptionHandler(app.Logger);

app.UseSwagger();
app.UseSwaggerUI();

var todoGroup = app.MapGroup("/todo");

todoGroup.MapGet("/", async (TodoDbContext db, ILogger<Program> logger) =>
{
    logger.LogInformation("Listando todas as tarefas");
    return Results.Ok(await db.TodoItems.AsNoTracking().ToListAsync());
});

todoGroup.MapGet("/{id:Guid}", async (Guid id, TodoDbContext db, ILogger<Program> logger) =>
{
    var todo = await db.TodoItems.FindAsync(id);
    if (todo == null)
    {
        logger.LogWarning("Tarefa não encontrada: {Id}", id);
        return Results.NotFound(new ErrorResponse
        {
            Timestamp = DateTime.UtcNow,
            StatusCode = StatusCodes.Status404NotFound,
            Message = "Tarefa não encontrada."
        });
    }

    return Results.Ok(todo);
});

todoGroup.MapPost("/", async ([FromBody] TodoItemDto dto, TodoDbContext db) =>
{
    if (!dto.TryValidate(out var errors))
    {
        return Results.BadRequest(new ErrorResponse
        {
            Timestamp = DateTime.UtcNow,
            StatusCode = StatusCodes.Status400BadRequest,
            Message = "Validation failed",
            Errors = errors
        });
    }

    var todo = new TodoItem
    {
        Title = dto.Title,
        IsDone = dto.IsDone
    };

    db.TodoItems.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/todo/{todo.Id}", todo);
});

todoGroup.MapPut("/{id:Guid}", async (Guid id, [FromBody] TodoItemDto dto, TodoDbContext db) =>
{
    if (!dto.TryValidate(out var errors))
    {
        return Results.BadRequest(new ErrorResponse
        {
            Timestamp = DateTime.UtcNow,
            StatusCode = StatusCodes.Status400BadRequest,
            Message = "Validation failed",
            Errors = errors
        });
    }

    var todo = await db.TodoItems.FindAsync(id);

    if (todo == null)
        return Results.NotFound(new ErrorResponse
        {
            Timestamp = DateTime.UtcNow,
            StatusCode = StatusCodes.Status404NotFound,
            Message = "Tarefa não encontrada."
        });

    todo.Title = dto.Title;
    todo.IsDone = dto.IsDone;

    await db.SaveChangesAsync();

    return Results.Ok(todo);
});

todoGroup.MapDelete("/{id:Guid}", async (Guid id, TodoDbContext db) =>
{
    var todo = await db.TodoItems.FindAsync(id);

    if (todo == null)
        return Results.NotFound(new ErrorResponse
        {
            Timestamp = DateTime.UtcNow,
            StatusCode = StatusCodes.Status404NotFound,
            Message = "Tarefa não encontrada."
        });

    db.TodoItems.Remove(todo);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();