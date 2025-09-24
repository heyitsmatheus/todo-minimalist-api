using Microsoft.EntityFrameworkCore;
using Todo.Minimalist.Api.Data;
using Todo.Minimalist.Api.DTOs;
using Todo.Minimalist.Api.Entities;
using Todo.Minimalist.Api.Extensions;
using Todo.Minimalist.Api.Models.Errors;

namespace Todo.Minimalist.Api.Endpoints;

public static class TodoEndpoints
{
    public static IEndpointRouteBuilder MapTodoEndpoints(this IEndpointRouteBuilder routes)
    {
        var todoGroup = routes.MapGroup("/todo")
            .RequireAuthorization();

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

        todoGroup.MapPost("/", async (TodoItemDto dto, TodoDbContext db) =>
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

        todoGroup.MapPut("/{id:Guid}", async (Guid id, TodoItemDto dto, TodoDbContext db) =>
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

        return routes;
    }
}
