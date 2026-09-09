using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Plugin.Avalonia.MVVMExpress.Hosting;
using Plugin.Avalonia.MVVMExpress.Navigation;
using Plugin.Avalonia.MVVMExpress.Threading;

namespace Plugin.Avalonia.MVVMExpress.Dialogs;

/// <summary>Shows a toast via <see cref="WindowNotificationManager"/>. Never replaces <c>Window.Content</c>.</summary>
public sealed class AvaloniaToastPresenter : IToastPresenter
{
    private readonly IWindowContext _window;
    private readonly IMainThread _main;

    public AvaloniaToastPresenter(IWindowContext? window = null, IMainThread? mainThread = null)
    {
        _window = window ?? WindowContext.Default;
        _main = mainThread ?? NotificationMarshaller.Current ?? ImmediateMainThread.Instance;
    }

    public Task ShowAsync(string message, TimeSpan duration, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message);
        cancellationToken.ThrowIfCancellationRequested();
        return _main.InvokeAsync(() =>
        {
            var owner = AvaloniaWindowContext.TryGetWindow(_window);
            if (owner is null)
            {
                return;
            }

            var manager = new WindowNotificationManager(owner) { Position = NotificationPosition.BottomCenter };
            manager.Show(new Notification(null, message, NotificationType.Information, duration));
        }, cancellationToken);
    }
}
