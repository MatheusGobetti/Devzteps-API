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
