using System.Runtime.CompilerServices;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Plugin.Avalonia.MVVMExpress.Navigation;

namespace Plugin.Avalonia.MVVMExpress.Hosting;

/// <summary>Maps an Avalonia <see cref="Window"/> to an <see cref="IWindowContext"/>.</summary>
public static class AvaloniaWindowContext
{
    private static readonly ConditionalWeakTable<Window, WindowContext> Map = [];
    private static int _next;

    public static IWindowContext For(Window window)
    {
        ArgumentNullException.ThrowIfNull(window);
        return Map.GetValue(window, static _ => new WindowContext($"window-{Interlocked.Increment(ref _next)}"));
    }

    public static IWindowContext Current
    {
        get
        {
            var window = TryMainWindow();
            return window is null ? WindowContext.Default : For(window);
        }
    }

    public static Window? TryGetWindow(IWindowContext? context)
    {
        var lifetime = global::Avalonia.Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
        var windows = lifetime?.Windows;
        if (windows is null)
        {
            return TryMainWindow();
        }

        if (context is not null)
        {
            foreach (var window in windows)
            {
                if (Map.TryGetValue(window, out var mapped) &&
                    string.Equals(mapped.WindowId, context.WindowId, StringComparison.Ordinal))
                {
                    return window;
                }
            }
        }

        return TryMainWindow();
    }

    private static Window? TryMainWindow()
        => (global::Avalonia.Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
}
