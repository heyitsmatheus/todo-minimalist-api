using Microsoft.EntityFrameworkCore;
using Todo.Minimalist.Api.Data;
using Todo.Minimalist.Api.Middlewares;
using Todo.Minimalist.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();


app.MapGet("/todos", async (TodoDbContext db) =>
    await db.TodoItems.ToListAsync());

app.MapGet("/todos/{id}", async (Guid id, TodoDbContext db) =>
    await db.TodoItems.FindAsync(id) is TodoItem todo
        ? Results.Ok(todo)
        : Results.NotFound());

app.MapPost("/todos", async (TodoItem todo, TodoDbContext db) =>
{
    todo.Id = Guid.NewGuid();
    db.TodoItems.Add(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/todos/{todo.Id}", todo);
});

app.MapPut("/todos/{id}", async (Guid id, TodoItem updatedTodo, TodoDbContext db) =>
{
    var todo = await db.TodoItems.FindAsync(id);
    
    if (todo is null) 
        return Results.NotFound();

    todo.Title = updatedTodo.Title;
    todo.IsDone = updatedTodo.IsDone;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/todos/{id}", async (Guid id, TodoDbContext db) =>
{
    var todo = await db.TodoItems.FindAsync(id);
    
    if (todo is null) 
        return Results.NotFound();

    db.TodoItems.Remove(todo);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();