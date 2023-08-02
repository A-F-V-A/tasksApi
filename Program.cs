using CourseEF;
using CourseEF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlServer<TaskContext>(builder.Configuration.GetConnectionString("dbConnection"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async ([FromServices] TaskContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("DataBase in memory: " + dbContext.Database.IsInMemory());
});

app.MapGet("/api/tasks/",  ([FromServices] TaskContext dbContext) =>
{
    return Results.Ok( dbContext.Tasks.Include(i => i.Category));
});

app.MapPost("/api/tasks/", async ([FromServices] TaskContext dbContext, [FromBody] Tasks tasks) =>
{
    tasks.TasksId = Guid.NewGuid();
    tasks.CreationDate = DateTime.Now;
    await dbContext.AddAsync(tasks);
    await dbContext.SaveChangesAsync();
    return Results.Ok();
});

app.MapPut("/api/tasks/{id}", async ([FromServices] TaskContext dbContext, [FromBody] Tasks tasks, [FromRoute] Guid id) =>
{
    Tasks findTask = dbContext.Tasks.Find(id)!;

    if (findTask == null) return Results.NotFound();

    findTask.CategoryId = tasks.CategoryId;
    findTask.Title = tasks.Title;
    findTask.Description = tasks.Description;
    findTask.Priority = tasks.Priority;
    await dbContext.SaveChangesAsync();

    return Results.Ok();
});

app.MapDelete("/api/tasks/{id}", async ([FromServices] TaskContext dbContext,[FromRoute] Guid id) =>
{
    Tasks findTask = dbContext.Tasks.Find(id)!;

    if (findTask == null) return Results.NotFound();

    dbContext.Remove(findTask);
    await dbContext.SaveChangesAsync();

    return Results.Ok();
});

app.Run(app.Configuration["HOST"]);
