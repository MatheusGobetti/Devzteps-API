using Devzteps_API.Application.Interfaces.Todo;
using Devzteps_API.Data;
using Devzteps_API.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Devzteps_API.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            // Registra o DbContext com EF Core e SQL Server
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Registrar os repositórios
            services.AddScoped<ITodoRepository, TodoRepository>();

            return services;
        }
    }
}
