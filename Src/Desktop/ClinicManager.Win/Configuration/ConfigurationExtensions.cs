using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicManager.Win.Configuration;

public static class ConfigurationExtensions
{
    /// <summary>
    /// Registers ApplicationConfiguration. Precedence (lowest to highest):
    /// appsettings.json -> appsettings.{env}.json -> environment variables
    /// -> command line -> code (optional configure callback).
    /// </summary>
    public static IServiceCollection AddCmConfig(this IServiceCollection services)
        => services.AddCmConfig(null);

    /// <summary>
    /// Same as above, with an optional code override applied last.
    /// </summary>
    public static IServiceCollection AddCmConfig(
        this IServiceCollection services,
        Action<ApplicationConfiguration>? configure)
    {
        var configuration = GetOrBuildConfiguration(services);

        services.AddOptions<ApplicationConfiguration>()
            .Bind(configuration.GetSection(ApplicationConfiguration.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        if (configure is not null)
        {
            // Runs after binding, so code wins over every other source.
            services.PostConfigure(configure);
        }

        return services;
    }

    private static IConfiguration GetOrBuildConfiguration(IServiceCollection services)
    {
        // Generic host / WebApplication already registers an IConfiguration
        // (json, env vars, command line). Reuse it if present.
        var existing = services
            .LastOrDefault(d => d.ServiceType == typeof(IConfiguration))
            ?.ImplementationInstance as IConfiguration;

        if (existing is not null)
        {
            return existing;
        }

        var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
                  ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                  ?? "Production";

        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddCommandLine(Environment.GetCommandLineArgs().Skip(1).ToArray())
            .Build();

        services.AddSingleton<IConfiguration>(configuration);
        return configuration;
    }
}
