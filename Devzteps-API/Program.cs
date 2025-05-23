using Devzteps_API.Application;
using Devzteps_API.Infrastructure;
using Devzteps_API.Infrastructure.Extensions;
using FluentMigrator.Runner;

var builder = WebApplication.CreateBuilder(args);

// Connection string (você pode pegar do appsettings.json)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adicionar injeções organizadas por camada
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(connectionString);

// Adiciona os serviços do FluentMigrator de forma limpa
builder.Services.AddDatabaseMigrations(builder.Configuration);

builder.Logging.ClearProviders(); // Remove os providers padrões
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Warning); // Silencia "info" e "debug"

ThreadPool.SetMinThreads(100, 100);
ThreadPool.GetMinThreads(out var worker, out var io);
Console.WriteLine($"[ThreadPool] Min threads: {worker}, IO threads: {io}");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.RunMigrations();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
