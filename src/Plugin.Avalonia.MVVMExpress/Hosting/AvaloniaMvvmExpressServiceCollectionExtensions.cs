using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
#if DEBUG
using Plugin.Avalonia.MVVMExpress.Diagnostics;
#endif
using Plugin.Avalonia.MVVMExpress.Generated;
using Plugin.Avalonia.MVVMExpress.Navigation;
using Plugin.Avalonia.MVVMExpress.Threading;

namespace Plugin.Avalonia.MVVMExpress.Hosting;

/// <summary>Avalonia host entry point.</summary>
public static class AvaloniaMvvmExpressServiceCollectionExtensions
{
    /// <summary>Registers Core plus the family dispatcher, window context, and lifecycle.</summary>
    public static IServiceCollection AddAvaloniaMvvmExpress(
        this IServiceCollection services,
        Action<MvvmExpressOptions>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(services);
        var options = new MvvmExpressOptions();
        configure?.Invoke(options);
        return UseAvaloniaMvvmExpress(services, options);
    }

    /// <summary>Registers MVVMExpress on <paramref name="builder"/>.</summary>
    public static IHostApplicationBuilder UseAvaloniaMvvmExpress(
        this IHostApplicationBuilder builder,
        Action<MvvmExpressOptions>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.Services.AddAvaloniaMvvmExpress(configure);
        return builder;
    }

    /// <summary>Registers MVVMExpress on <paramref name="services"/>.</summary>
    public static IServiceCollection UseAvaloniaMvvmExpress(
        this IServiceCollection services,
        Action<MvvmExpressOptions>? configure)
    {
        ArgumentNullException.ThrowIfNull(services);
        var options = new MvvmExpressOptions();
        configure?.Invoke(options);
        return UseAvaloniaMvvmExpress(services, options);
    }

    private static IServiceCollection UseAvaloniaMvvmExpress(IServiceCollection services, MvvmExpressOptions options)
    {
        services.AddSingleton(options);
        services.AddMvvmExpress();
        var main = new AvaloniaDispatcherMainThread();
        services.RemoveAll<IMainThread>();
        services.AddSingleton<IMainThread>(main);
        NotificationMarshaller.Current = main;
        NotificationMarshaller.MarshalNotifications = options.MarshalNotifications;
        services.RemoveAll<IWindowContext>();
        services.AddSingleton<IWindowContext>(_ => AvaloniaWindowContext.Current);
#if DEBUG
        if (options.EnableDiagnostics)
        {
            var diagnostics = new CallbackDiagnostics(static (area, message) =>
                System.Diagnostics.Debug.WriteLine($"[MVVMExpress:{area}] {message}"));
            services.RemoveAll<IMvvmExpressDiagnostics>();
            services.AddSingleton<IMvvmExpressDiagnostics>(_ => diagnostics);
            NotificationMarshaller.Diagnostics = diagnostics;
        }
#endif
        options.ApplyRegistrations(services);
        if (options.ApplyGeneratedRegistrations)
        {
            GeneratedRegistrationHooks.Apply(services);
        }

        if (options.AuthChallengeViewModel is { } challenge)
        {
            services.AddAuth(challenge, options.ForwardNavigationFailures);
        }

        return services;
    }
}
