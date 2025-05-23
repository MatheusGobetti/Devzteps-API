using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Devzteps_API.Infrastructure.Extensions
{
    public static class FluentMigratorExtensions
    {
        public static IServiceCollection AddDatabaseMigrations(this IServiceCollection services, IConfiguration configuration)
        {
            // Nome do assembly onde estão as migrations
            const string migrationsAssemblyName = "Devzteps-API.Data";

            // Carrega o assembly pelo nome (assegure que o assembly está carregado em runtime)
            Assembly migrationsAssembly = null;

            try
            {
                migrationsAssembly = Assembly.Load(migrationsAssemblyName);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Falha ao carregar o assembly '{migrationsAssemblyName}'. Certifique-se que ele está compilado e referenciado.", ex);
            }

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(configuration.GetConnectionString("DefaultConnection"))
                    .ScanIn(migrationsAssembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole());

            return services;
        }
    }
}
