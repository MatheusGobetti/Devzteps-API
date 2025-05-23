using Devzteps_API.Application.Interfaces.Todo;
using Devzteps_API.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Devzteps_API.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Registrar os repositórios
            services.AddScoped<ITodoRepository, TodoRepository>();

            return services;
        }
    }
}
