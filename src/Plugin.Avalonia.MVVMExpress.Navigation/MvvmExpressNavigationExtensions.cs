using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Plugin.Avalonia.MVVMExpress.Diagnostics;
using Plugin.Avalonia.MVVMExpress.Generated;
using Plugin.Avalonia.MVVMExpress.Hosting;
using Plugin.Avalonia.MVVMExpress.Threading;

namespace Plugin.Avalonia.MVVMExpress.Navigation;

public static class MvvmExpressNavigationExtensions
{
    public static MvvmExpressOptions UseFrameNavigation(
        this MvvmExpressOptions options,
        Action<AvaloniaFrameNavigator, IServiceProvider>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(options);
        return options.AddRegistration(services =>
        {
            services.RemoveAll<INavigator>();
            services.RemoveAll<IPageNavigator>();
            services.AddSingleton<AvaloniaFrameNavigator>(sp =>
            {
                var window = sp.GetService<IWindowContext>();
                var navigator = new AvaloniaFrameNavigator(
                    window,
                    sp,
                    () => AvaloniaVisualTree.CurrentFrame(window),
                    sp.GetService<IMainThread>(),
                    sp.GetService<IMvvmExpressDiagnostics>(),
                    sp.GetService<MvvmExpressOptions>());
                GeneratedRegistrationHooks.ApplyPageMaps((vm, view, route) => navigator.Map(vm, view, route));
                configure?.Invoke(navigator, sp);
                return navigator;
            });
            services.AddSingleton<IPageNavigator>(sp => sp.GetRequiredService<AvaloniaFrameNavigator>());
            services.AddSingleton<INavigator>(sp => sp.GetRequiredService<AvaloniaFrameNavigator>());
        });
    }
}
