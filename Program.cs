using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using WebApiEntityFrameworkDockerSqlServer.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<SalesContext>(options =>
    options.UseNpgsql($"Server = {Environment.GetEnvironmentVariable("DB_HOST")};" +
    $" Port = {Environment.GetEnvironmentVariable("DB_PORT")}; Database = {Environment.GetEnvironmentVariable("DB_NAME")};" +
    $" User Id = {Environment.GetEnvironmentVariable("DB_USER")}; Password = {Environment.GetEnvironmentVariable("DB_PASSWORD")}"));
//builder.Services.AddDbContext<SalesContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("PglSalesDb")));
var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var salesContext = scope.ServiceProvider.GetRequiredService<SalesContext>();
    salesContext.Database.EnsureCreated();

    salesContext.Seed();
}

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
