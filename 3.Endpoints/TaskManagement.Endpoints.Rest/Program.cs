using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.ApplicationServices;
using TaskManagement.Core.Contracts.Services;
using TaskManagement.Core.Domain.Repositories;
using TaskManagement.Core.Domain.Tasks;
using TaskManagement.Infra.Data.EF.SqlServer;
using TaskManagement.Infra.Data.EF.SqlServer.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConn")));

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

app.Run(async context =>
{
    var path = context.Request.Path.ToString().ToLower();
    var method = context.Request.Method.ToUpper();
    var taskService = context.RequestServices.GetRequiredService<ITaskService>();

    // GET /tasks
    if (path == "/tasks" && method == "GET")
    {
        var tasks = await taskService.GetAllAsync();
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(tasks);
        return;
    }

    // GET /tasks/{id}
    if (path.StartsWith("/tasks/") && method == "GET")
    {
        if (int.TryParse(path.Split("/")[2], out int id))
        {
            var task = await taskService.GetByIdAsync(id);
            if (task == null)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("Not Found");
                return;
            }
            await context.Response.WriteAsJsonAsync(task);
            return;
        }
    }

    // POST /tasks
    if (path == "/tasks" && method == "POST")
    {
        var task = await context.Request.ReadFromJsonAsync<Taska>();
        if (task == null)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Bad Request");
            return;
        }
        var result = await taskService.CreateAsync(task);
        context.Response.StatusCode = 201;
        await context.Response.WriteAsJsonAsync(result);
        return;
    }

    // PUT /tasks/{id}
    if (path.StartsWith("/tasks/") && method == "PUT")
    {
        if (int.TryParse(path.Split("/")[2], out int id))
        {
            var updated = await context.Request.ReadFromJsonAsync<Taska>();
            var ok = await taskService.UpdateAsync(id, updated);
            context.Response.StatusCode = ok ? 204 : 404;
            return;
        }
    }

    // DELETE /tasks/{id}
    if (path.StartsWith("/tasks/") && method == "DELETE")
    {
        if (int.TryParse(path.Split("/")[2], out int id))
        {
            var ok = await taskService.DeleteAsync(id);
            context.Response.StatusCode = ok ? 204 : 404;
            return;
        }
    }

    // مسیر ناشناس
    context.Response.StatusCode = 404;
    await context.Response.WriteAsync("Not Found");
});

app.Run();