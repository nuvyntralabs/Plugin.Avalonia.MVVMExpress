using Avalonia.Controls;
using Plugin.Avalonia.MVVMExpress.ComponentModel;
using Plugin.Avalonia.MVVMExpress.Navigation;
using Plugin.Avalonia.MVVMExpress.Threading;

namespace Plugin.Avalonia.MVVMExpress.Navigation.Tests;

public sealed class FrameNavigatorTests
{
    [Fact]
    public async Task ResetAsync_WithoutFrame_FailsWithE_PAGE()
    {
        var navigator = new AvaloniaFrameNavigator(frame: () => null, mainThread: ImmediateMainThread.Instance)
            .Map<HomeViewModel, HomeView>("home");

        var result = await navigator.ResetAsync<HomeViewModel>();

        Assert.False(result.IsSuccess);
        Assert.Equal("E_PAGE", result.Error?.Code);
    }

    [Fact]
    public async Task ResetAsync_ReplacesRoot_SoBackIsBlocked()
    {
        var frame = new Plugin.Avalonia.MVVMExpress.Controls.Frame();
        var navigator = new AvaloniaFrameNavigator(frame: () => frame, mainThread: ImmediateMainThread.Instance)
            .Map<HomeViewModel, HomeView>("home")
            .Map<DetailsViewModel, DetailsView>("details");

        var login = await navigator.ResetAsync<HomeViewModel>();
        Assert.True(login.IsSuccess, login.Error?.Message);
        Assert.Equal(typeof(HomeViewModel), navigator.Current);
        Assert.False(navigator.CanGoBack);

        var details = await navigator.NavigateToAsync<DetailsViewModel>();
        Assert.True(details.IsSuccess, details.Error?.Message);
        Assert.True(navigator.CanGoBack);

        var home = await navigator.ResetAsync<HomeViewModel>();
        Assert.True(home.IsSuccess, home.Error?.Message);
        Assert.Equal(typeof(HomeViewModel), navigator.Current);
        Assert.False(navigator.CanGoBack);
    }

    [Fact]
    public async Task Navigate_ConstructsView_OnMainThread()
    {
        var frame = new Plugin.Avalonia.MVVMExpress.Controls.Frame();
        var thread = new RecordingMainThread();
        var navigator = new AvaloniaFrameNavigator(frame: () => frame, mainThread: thread)
            .Map<HomeViewModel, HomeView>("home");

        var result = await navigator.ResetAsync<HomeViewModel>();

        Assert.True(result.IsSuccess, result.Error?.Message);
        Assert.True(thread.Invoked);
    }

    private sealed class HomeViewModel : ViewModel
    {
    }

    private sealed class DetailsViewModel : ViewModel
    {
    }

    private sealed class HomeView : UserControl
    {
    }

    private sealed class DetailsView : UserControl
    {
    }

    private sealed class RecordingMainThread : IMainThread
    {
        public bool Invoked { get; private set; }

        public bool IsMainThread => true;

        public void BeginInvoke(Action action)
        {
            Invoked = true;
            action();
        }

        public Task InvokeAsync(Action action, CancellationToken cancellationToken = default)
        {
            Invoked = true;
            action();
            return Task.CompletedTask;
        }

        public Task InvokeAsync(Func<Task> action, CancellationToken cancellationToken = default)
        {
            Invoked = true;
            return action();
        }
    }
}
