using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Plugin.Avalonia.MVVMExpress.Auth;
using Plugin.Avalonia.MVVMExpress.Dialogs;
using Plugin.Avalonia.MVVMExpress.Hosting;
using Plugin.Avalonia.MVVMExpress.Navigation;
using App1.Pages;

namespace App1;

public partial class App : Application
{
    private IHost? _host;

    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override async void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var builder = Host.CreateApplicationBuilder();
            builder.Services.AddSingleton<IAuthState, DemoAuthState>();
            builder.Services.AddSingleton<IItemStore, MemoryItemStore>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<DetailsViewModel>();
            builder.Services.AddTransient<EditViewModel>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<DetailsPage>();
            builder.Services.AddTransient<EditPage>();
            builder.Services.AddSingleton<MainWindow>();
            builder.Services.UseAvaloniaMvvmExpress(o => o
                .UseFrameNavigation((nav, _) => nav
                    .Map<LoginViewModel, LoginPage>("login")
                    .Map<HomeViewModel, HomePage>("home")
                    .Map<DetailsViewModel, DetailsPage>("details")
                    .Map<EditViewModel, EditPage>("edit"))
                .UseDialogs()
                .UseAuth<LoginViewModel>());
            _host = builder.Build();
            await _host.StartAsync().ConfigureAwait(true);
            desktop.MainWindow = _host.Services.GetRequiredService<MainWindow>();
            var navigator = _host.Services.GetRequiredService<INavigator>();
            await navigator.ResetAsync<LoginViewModel>().ConfigureAwait(true);
        }

        base.OnFrameworkInitializationCompleted();
    }
}
