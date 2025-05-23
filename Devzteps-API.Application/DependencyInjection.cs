using Devzteps_API.Application.Interfaces.Todo;
using Devzteps_API.Application.Mapping;
using Devzteps_API.Application.Services.Todo;
using Microsoft.Extensions.DependencyInjection;

namespace Devzteps_API.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Registrar AutoMapper e Profile
            services.AddAutoMapper(typeof(TodoMappingProfile));

            // Registrar as services da Application
            services.AddScoped<ITodoService, TodoService>();

            return services;
        }
    }
}
