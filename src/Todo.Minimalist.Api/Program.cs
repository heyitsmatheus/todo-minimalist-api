using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Todo.Minimalist.Api.Data;
using Todo.Minimalist.Api.DTOs;
using Todo.Minimalist.Api.Entities;
using Todo.Minimalist.Api.Middlewares;

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

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/todo", async (TodoDbContext db, ILogger<Program> logger) =>
{
    logger.LogInformation("Listando todas as tarefas");
    return Results.Ok(await db.TodoItems.AsNoTracking().ToListAsync());
});

app.MapGet("/todo/{id:Guid}", async (Guid id, TodoDbContext db, ILogger<Program> logger) =>
{
    var todo = await db.TodoItems.FindAsync(id);
    if (todo == null)
    {
        logger.LogWarning("Tarefa não encontrada: {Id}", id);
        return Results.NotFound(new { message = "Tarefa não encontrada." });
    }
    return Results.Ok(todo);
});

app.MapPost("/todo", async (
    [FromBody] TodoItemDto dto,
    TodoDbContext db,
    HttpContext http) =>
{
    var validationResults = new List<ValidationResult>();
    if (!Validator.TryValidateObject(dto, new ValidationContext(dto), validationResults, true))
    {
        var errors = validationResults.ToDictionary(
            v => v.MemberNames.FirstOrDefault() ?? "",
            v => new[] { v.ErrorMessage ?? "Invalid field" });

        return Results.ValidationProblem(errors);
    }

    var todo = new TodoItem { Title = dto.Title, IsDone = dto.IsDone };

    db.TodoItems.Add(todo);

    await db.SaveChangesAsync();
    return Results.Created($"/todo/{todo.Id}", todo);
});

app.MapPut("/todo/{id:Guid}", async (
    Guid id,
    [FromBody] TodoItemDto dto,
    TodoDbContext db,
    HttpContext http) =>
{
    var validationResults = new List<ValidationResult>();
    if (!Validator.TryValidateObject(dto, new ValidationContext(dto), validationResults, true))
    {
        var errors = validationResults.ToDictionary(
            v => v.MemberNames.FirstOrDefault() ?? "",
            v => new[] { v.ErrorMessage ?? "Invalid field" });

        return Results.ValidationProblem(errors);
    }

    var todo = await db.TodoItems.FindAsync(id);

    if (todo == null)
        return Results.NotFound(new { message = "Tarefa não encontrada." });

    todo.Title = dto.Title;
    todo.IsDone = dto.IsDone;

    await db.SaveChangesAsync();
    return Results.Ok(todo);
});

app.MapDelete("/todo/{id:Guid}", async (Guid id, TodoDbContext db) =>
{
    var todo = await db.TodoItems.FindAsync(id);

    if (todo == null)
        return Results.NotFound(new { message = "Tarefa não encontrada." });

    db.TodoItems.Remove(todo);

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();