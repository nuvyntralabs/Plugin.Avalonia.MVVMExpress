using Plugin.Avalonia.MVVMExpress.Dialogs;
using Plugin.Avalonia.MVVMExpress.Navigation;
using Plugin.Avalonia.MVVMExpress.Threading;

namespace Plugin.Avalonia.MVVMExpress.Dialogs.Tests;

public sealed class ToastPresenterTests
{
    [Fact]
    public async Task Toast_WithoutOwnerWindow_DoesNotThrow()
    {
        var presenter = new AvaloniaToastPresenter(WindowContext.Default, ImmediateMainThread.Instance);
        await presenter.ShowAsync("Saved", TimeSpan.FromMilliseconds(1));
    }
}
