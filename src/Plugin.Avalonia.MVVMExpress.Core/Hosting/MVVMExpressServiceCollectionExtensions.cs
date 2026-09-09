using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Plugin.Avalonia.MVVMExpress.Busy;
using Plugin.Avalonia.MVVMExpress.Caching;
using Plugin.Avalonia.MVVMExpress.Composition;
using Plugin.Avalonia.MVVMExpress.Connectivity;
using Plugin.Avalonia.MVVMExpress.Dialogs;
using Plugin.Avalonia.MVVMExpress.Errors;
using Plugin.Avalonia.MVVMExpress.Files;
using Plugin.Avalonia.MVVMExpress.Flags;
using Plugin.Avalonia.MVVMExpress.Media;
using Plugin.Avalonia.MVVMExpress.Messaging;
using Plugin.Avalonia.MVVMExpress.Navigation;
using Plugin.Avalonia.MVVMExpress.Operations;
using Plugin.Avalonia.MVVMExpress.Permissions;
using Plugin.Avalonia.MVVMExpress.Diagnostics;
using Plugin.Avalonia.MVVMExpress.State;
using Plugin.Avalonia.MVVMExpress.Threading;

namespace Plugin.Avalonia.MVVMExpress.Hosting;

/// <summary>Registers Core services for tests, samples, and <c>UseMvvmExpress</c>.</summary>
public static class MVVMExpressServiceCollectionExtensions
{
    /// <summary>Adds Core singletons used by ViewModels.</summary>
    /// <param name="services">Service collection.</param>
    public static IServiceCollection AddMvvmExpress(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.TryAddSingleton<IMessageHub, MessageHub>();
        services.TryAddSingleton<IBusyGate, BusyGate>();
        services.TryAddSingleton<IErrorSink, NullErrorSink>();
        services.TryAddSingleton<ICache, MemoryCache>();
        services.TryAddSingleton<IConnectivityProbe, InMemoryConnectivityProbe>();
        services.TryAddSingleton<IWindowContext>(_ => WindowContext.Default);
        services.TryAddSingleton<IWindowNavigatorRegistry, WindowNavigatorRegistry>();
        services.TryAddSingleton<InMemoryNavigator>();
        services.TryAddSingleton<INavigator>(sp => sp.GetRequiredService<InMemoryNavigator>());
        services.TryAddSingleton<IPageNavigator>(sp => sp.GetRequiredService<InMemoryNavigator>());
        services.TryAddSingleton<IMainThread>(_ => ImmediateMainThread.Instance);
        services.TryAddSingleton<IDialogs, NullDialogs>();
        services.TryAddSingleton<INotifier>(_ => NullDialogs.Instance);
        services.TryAddSingleton<ICachedFetcher>(sp =>
            new CachedFetcher(sp.GetRequiredService<ICache>(), sp.GetService<IConnectivityProbe>()));
        services.TryAddSingleton<IOperationExecutor>(sp =>
            new OperationExecutor(sp.GetService<IBusyGate>(), sp.GetService<IErrorSink>()));
        services.TryAddSingleton<IViewModelScopeFactory>(sp => new ServiceViewModelScopeFactory(sp));
        services.TryAddSingleton<IFeatureSwitch, MemoryFeatureSwitch>();
        services.TryAddSingleton<IPermissionGate>(_ => AllowAllPermissionGate.Instance);
        services.TryAddSingleton<IFileStore, MemoryFileStore>();
        services.TryAddSingleton<IMediaPicker>(_ => NullMediaPicker.Instance);
        services.TryAddSingleton<IStateStore, MemoryStateStore>();
        services.TryAddSingleton<IMvvmExpressDiagnostics>(_ => NullDiagnostics.Instance);
        return services;
    }
}
