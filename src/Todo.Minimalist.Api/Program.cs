using Microsoft.EntityFrameworkCore;
using Todo.Minimalist.Api.Data;
using Todo.Minimalist.Api.Endpoints;
using Todo.Minimalist.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlite("Data Source=Data/todo.db"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseGlobalExceptionHandler(app.Logger);

app.UseSwagger();
app.UseSwaggerUI();

app.MapTodoEndpoints();

app.Run();