using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Plugin.Avalonia.MVVMExpress.Hosting;

namespace Plugin.Avalonia.MVVMExpress.Dialogs;

public static class MvvmExpressDialogsExtensions
{
    /// <summary>Registers <see cref="IDialogs"/> and <see cref="INotifier"/>.</summary>
    public static MvvmExpressOptions UseDialogs(this MvvmExpressOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        return options.AddRegistration(static services =>
        {
            services.RemoveAll<IDialogs>();
            services.RemoveAll<INotifier>();
            services.AddSingleton<IDialogs, AvaloniaDialogs>();
            services.AddSingleton<INotifier, AvaloniaNotifier>();
        });
    }
}
