using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UE.Core.Attributes;

namespace UE.Core.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Scans and registers services in the specified assembly.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to register the services with.</param>
    /// <param name="assemblies">The assemblies to scan</param>
    /// <returns></returns>
    public static IServiceCollection ScanAndRegisterServices(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.Scan(x => x
            .FromCallingAssembly()
            .FromAssemblies(assemblies)
            .AddClasses(c => c.WithAttribute<ScopedServiceAttribute>())
            .AsMatchingInterface()
            .WithScopedLifetime()

            .AddClasses(c => c.WithAttribute<SingletonServiceAttribute>())
            .AsMatchingInterface()
            .WithSingletonLifetime()

            .AddClasses(c => c.WithAttribute<TransientServiceAttribute>())
            .AsMatchingInterface()
            .WithTransientLifetime()

            .AddClasses(c => c.WithAttribute<ScopedSelfServiceAttribute>())
            .AsSelf()
            .WithScopedLifetime()

            .AddClasses(c => c.WithAttribute<SingletonSelfServiceAttribute>())
            .AsSelf()
            .WithSingletonLifetime()

            .AddClasses(c => c.WithAttribute<TransientSelfServiceAttribute>())
            .AsSelf()
            .WithTransientLifetime());

        return services;
    }

    /// <summary>
    /// Scans and registers handlers (Command, Query) in the specified assembly.
    /// NOTE: Handlers are registered with transient lifetime
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to register the services with.</param>
    /// /// <param name="assemblies">The assemblies to scan</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection ScanAndRegisterHandlers(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.Scan(x => x
            .FromCallingAssembly()
            .FromAssemblies(assemblies)
            .AddClasses(c => c.WithAttribute<CommandHandlerAttribute>())
            .AsSelfWithInterfaces()
            .WithTransientLifetime()

            .AddClasses(c => c.WithAttribute<QueryHandlerAttribute>())
            .AsSelfWithInterfaces()
            .WithTransientLifetime());

        return services;
    }

    /// <summary>
    /// Scans and registers repositories in the specified assembly.
    /// NOTE: Repositories are registered with scoped lifetime
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to register the services with.</param>
    /// /// <param name="assemblies">The assemblies to scan</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection ScanAndRegisterRepositories(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.Scan(x => x
            .FromCallingAssembly()
            .FromAssemblies(assemblies)
            .AddClasses(c => c.WithAttribute<RepositoryAttribute>())
            .AsMatchingInterface()
            .WithScopedLifetime());

        return services;
    }
}
