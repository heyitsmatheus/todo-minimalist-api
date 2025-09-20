using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Todo.Minimalist.Api.Data;
using Todo.Minimalist.Api.DTOs;
using Todo.Minimalist.Api.Middlewares;
using Todo.Minimalist.Api.Models;
using Todo.Minimalist.Api.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlite("Data Source=Data/todo.db"));

builder.Services.AddValidatorsFromAssemblyContaining<TodoItemCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TodoItemUpdateDtoValidator>();

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();


// -------------------------------
// Endpoints
// -------------------------------

app.MapGet("/todos", async (TodoDbContext db, ILogger<Program> logger) =>
{
    var todos = await db.TodoItems.ToListAsync();
    
    logger.LogInformation("Listando {Count} todos", todos.Count);

    var readDtos = todos.Select(t => new TodoItemReadDto(t.Id, t.Title, t.IsDone)).ToList();

    return Results.Ok(readDtos);
});

app.MapGet("/todos/{id:guid}", async (Guid id, TodoDbContext db, ILogger<Program> logger) =>
{
    var todo = await db.TodoItems.FindAsync(id);
    if (todo == null)
    {
        logger.LogWarning("Todo não encontrado: {Id}", id);
        return Results.NotFound();
    }

    logger.LogInformation("Todo encontrado: {Id} - {Title}", todo.Id, todo.Title);
    
    var readDto = new TodoItemReadDto(id, todo.Title, todo.IsDone);

    return Results.Ok(readDto);
});

app.MapPost("/todos", async (
    TodoItemCreateDto dto,
    TodoDbContext db,
    IValidator<TodoItemCreateDto> validator,
    ILogger<Program> logger) =>
{
    ValidationResult result = await validator.ValidateAsync(dto);
    if (!result.IsValid)
    {
        logger.LogWarning("Validação falhou para criação: {Title}", dto.Title);
        var errors = result.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
        return Results.BadRequest(errors);
    }

    var todo = new TodoItem
    {
        Id = Guid.NewGuid(),
        Title = dto.Title,
        IsDone = dto.IsDone
    };

    db.TodoItems.Add(todo);
    await db.SaveChangesAsync();

    logger.LogInformation("Todo criado: {Id} - {Title}", todo.Id, todo.Title);

    var readDto = new TodoItemReadDto(todo.Id, todo.Title, todo.IsDone);

    return Results.Created($"/todos/{todo.Id}", readDto);
});

app.MapPut("/todos/{id:guid}", async (
    Guid id,
    TodoItemUpdateDto dto,
    TodoDbContext db,
    IValidator<TodoItemUpdateDto> validator,
    ILogger<Program> logger) =>
{
    ValidationResult result = await validator.ValidateAsync(dto);
    if (!result.IsValid)
    {
        logger.LogWarning("Validação falhou para atualização: {Title}", dto.Title);
        var errors = result.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
        return Results.BadRequest(errors);
    }

    var todo = await db.TodoItems.FindAsync(id);
    if (todo == null)
    {
        logger.LogWarning("Todo não encontrado para atualização: {Id}", id);
        return Results.NotFound();
    }

    todo.Title = dto.Title;
    todo.IsDone = dto.IsDone;

    await db.SaveChangesAsync();

    logger.LogInformation("Todo atualizado: {Id} - {Title}", todo.Id, todo.Title);

    var readDto = new TodoItemReadDto(todo.Id, todo.Title, todo.IsDone);

    return Results.Ok(readDto);
});

app.MapDelete("/todos/{id:guid}", async (Guid id, TodoDbContext db, ILogger<Program> logger) =>
{
    var todo = await db.TodoItems.FindAsync(id);
    if (todo == null)
    {
        logger.LogWarning("Todo não encontrado para exclusão: {Id}", id);
        return Results.NotFound();
    }

    db.TodoItems.Remove(todo);
    await db.SaveChangesAsync();

    logger.LogInformation("Todo excluído: {Id} - {Title}", todo.Id, todo.Title);
    return Results.NoContent();
});

app.Run();