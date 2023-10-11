using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TodoApp.Core.Helper;
using TodoApp.Infrastructure.Data;

namespace TodoApp.Api.Extensions
{
    public static class PersistenceExtention
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            #region Database
            AddDatabaseConfig(services, configuration);
            #endregion

            #region ConfigureLogs
            ConfigureLogs();
            #endregion

            #region configure strongly typed settings object
            ConfigureSettings(services, configuration);
            #endregion
            return services;
        }

        private static IServiceCollection AddDatabaseConfig(this IServiceCollection services,
           IConfiguration configuration)
        {
            var keyVaultEndpoint = configuration["AzureKeyVault:Endpoint"];
            if (!string.IsNullOrEmpty(keyVaultEndpoint))
            {
                var secretClient = new SecretClient(new Uri(keyVaultEndpoint), new DefaultAzureCredential());
                var secret = secretClient.GetSecret(configuration["AzureKeyVault:SecretName"]);

                // Add the connection string to your configuration
                configuration["ConnectionStrings:TodoAzureDatabase"] = secret.Value.Value;

            }
            string connectionString = configuration.GetConnectionString("TodoAzureDatabase") ?? String.Empty;
            // Configure PostgreSQL DbContext and inject options
            services.AddDbContext<TodoDbContext>(options =>
                options.UseSqlServer(connectionString));
            return services;
        }

        public static void ConfigureLogs()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Debug()
                .WriteTo.Console()
                .CreateLogger();
        }

        private static IServiceCollection ConfigureSettings(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            return services;
        }
    }
}
