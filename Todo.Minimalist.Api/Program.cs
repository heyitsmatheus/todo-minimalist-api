using Todo.Minimalist.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

var todos = new List<TodoItem>();

app.MapGet("/todos", () => todos);

app.MapGet("/todos/{id}", (Guid id) =>
    todos.FirstOrDefault(t => t.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());

app.MapPost("/todos", (TodoItem todo) =>
{
    var newTodo = todo with { Id = Guid.NewGuid() };
    todos.Add(newTodo);
    return Results.Created($"/todos/{newTodo.Id}", newTodo);
});

app.MapPut("/todos/{id}", (Guid id, TodoItem updated) =>
{
    var index = todos.FindIndex(t => t.Id == id);
    if (index == -1) return Results.NotFound();
    todos[index] = updated with { Id = id };
    return Results.NoContent();
});

app.MapDelete("/todos/{id}", (Guid id) =>
{
    var removed = todos.RemoveAll(t => t.Id == id);
    return removed > 0 ? Results.NoContent() : Results.NotFound();
});

app.Run();