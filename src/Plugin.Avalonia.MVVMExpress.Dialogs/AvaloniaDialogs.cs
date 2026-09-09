using Avalonia.Controls;
using Plugin.Avalonia.MVVMExpress.Hosting;
using Plugin.Avalonia.MVVMExpress.Navigation;
using Plugin.Avalonia.MVVMExpress.Outcome;
using Plugin.Avalonia.MVVMExpress.Threading;

namespace Plugin.Avalonia.MVVMExpress.Dialogs;

/// <summary>Avalonia modal <see cref="Window"/> adapter. No Win32 MessageBox.</summary>
public sealed class AvaloniaDialogs : IDialogs
{
    private readonly IWindowContext _window;
    private readonly IMainThread _main;

    public AvaloniaDialogs(IWindowContext? window = null, IMainThread? mainThread = null)
    {
        _window = window ?? WindowContext.Default;
        _main = mainThread ?? NotificationMarshaller.Current ?? ImmediateMainThread.Instance;
    }

    public Task AlertAsync(string title, string message, string cancel = "OK", CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return _main.InvokeAsync(() => ShowAsync(title, message, cancel, accept: null), cancellationToken);
    }

    public async Task<bool> ConfirmAsync(string title, string message, string accept = "OK", string cancel = "Cancel", CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var accepted = false;
        await _main.InvokeAsync(
            async () => accepted = await ShowAsync(title, message, cancel, accept).ConfigureAwait(true),
            cancellationToken).ConfigureAwait(false);
        return accepted;
    }

    public Task ErrorAsync(ErrorInfo error, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(error);
        return AlertAsync("Error", error.Message, cancellationToken: cancellationToken);
    }

    private async Task<bool> ShowAsync(string title, string message, string cancel, string? accept)
    {
        var owner = AvaloniaWindowContext.TryGetWindow(_window);
        var dialog = new Window
        {
            Title = title,
            Width = 360,
            Height = 180,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Content = BuildContent(message, cancel, accept, out var acceptClicked)
        };
        if (owner is not null)
        {
            await dialog.ShowDialog(owner).ConfigureAwait(true);
        }
        else
        {
            dialog.Show();
        }

        return acceptClicked();
    }

    private static Control BuildContent(string message, string cancel, string? accept, out Func<bool> accepted)
    {
        var result = false;
        accepted = () => result;
        var buttons = new StackPanel { Orientation = global::Avalonia.Layout.Orientation.Horizontal, Spacing = 8 };
        Window? host = null;
        var cancelBtn = new Button { Content = cancel };
        cancelBtn.Click += (_, _) => host?.Close();
        buttons.Children.Add(cancelBtn);
        if (accept is not null)
        {
            var acceptBtn = new Button { Content = accept };
            acceptBtn.Click += (_, _) =>
            {
                result = true;
                host?.Close();
            };
            buttons.Children.Add(acceptBtn);
        }

        var root = new StackPanel { Margin = new global::Avalonia.Thickness(16), Spacing = 16 };
        root.Children.Add(new TextBlock { Text = message, TextWrapping = global::Avalonia.Media.TextWrapping.Wrap });
        root.Children.Add(buttons);
        root.AttachedToVisualTree += (_, _) => host = TopLevel.GetTopLevel(root) as Window;
        return root;
    }
}
