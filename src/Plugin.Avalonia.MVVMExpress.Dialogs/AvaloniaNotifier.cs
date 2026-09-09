using Plugin.Avalonia.MVVMExpress.Navigation;

namespace Plugin.Avalonia.MVVMExpress.Dialogs;

/// <summary><see cref="INotifier"/> that shows an overlay toast.</summary>
public sealed class AvaloniaNotifier : INotifier
{
    private readonly IToastPresenter _presenter;

    public AvaloniaNotifier(IToastPresenter? presenter = null, IWindowContext? window = null)
        => _presenter = presenter ?? new AvaloniaToastPresenter(window);

    public Task ToastAsync(string message, TimeSpan? duration = null, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message);
        cancellationToken.ThrowIfCancellationRequested();
        return _presenter.ShowAsync(message, duration ?? TimeSpan.FromSeconds(2), cancellationToken);
    }
}
