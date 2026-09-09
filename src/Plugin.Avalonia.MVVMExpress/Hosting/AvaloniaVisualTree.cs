using Avalonia.Controls;
using Avalonia.VisualTree;
using Plugin.Avalonia.MVVMExpress.Controls;
using Plugin.Avalonia.MVVMExpress.Navigation;

namespace Plugin.Avalonia.MVVMExpress.Hosting;

/// <summary>Resolves the current <see cref="Frame"/> and content for a window.</summary>
public static class AvaloniaVisualTree
{
    public static Frame? CurrentFrame(IWindowContext? window = null)
    {
        var host = AvaloniaWindowContext.TryGetWindow(window);
        if (host is null)
        {
            return null;
        }

        if (host.FindControl<Frame>("NavigationHost") is { } named)
        {
            return named;
        }

        return host.GetVisualDescendants().OfType<Frame>().FirstOrDefault();
    }

    public static Control? CurrentView(IWindowContext? window = null)
    {
        var frame = CurrentFrame(window);
        if (frame?.Content is Control fromFrame)
        {
            return fromFrame;
        }

        return AvaloniaWindowContext.TryGetWindow(window)?.Content as Control;
    }
}
