using Avalonia.Threading;
using Plugin.Avalonia.MVVMExpress.Threading;

namespace Plugin.Avalonia.MVVMExpress.Threading;

/// <summary>Avalonia <see cref="Dispatcher"/> adapter for <see cref="IMainThread"/>.</summary>
public sealed class AvaloniaDispatcherMainThread : IMainThread
{
    private readonly Dispatcher _dispatcher;

    public AvaloniaDispatcherMainThread()
        : this(Dispatcher.UIThread)
    {
    }

    public AvaloniaDispatcherMainThread(Dispatcher dispatcher)
    {
        ArgumentNullException.ThrowIfNull(dispatcher);
        _dispatcher = dispatcher;
    }

    public bool IsMainThread => _dispatcher.CheckAccess();

    public void BeginInvoke(Action action)
    {
        ArgumentNullException.ThrowIfNull(action);
        if (_dispatcher.CheckAccess())
        {
            action();
            return;
        }

        _ = _dispatcher.InvokeAsync(action);
    }

    public Task InvokeAsync(Action action, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(action);
        cancellationToken.ThrowIfCancellationRequested();
        if (_dispatcher.CheckAccess())
        {
            action();
            return Task.CompletedTask;
        }

        return _dispatcher.InvokeAsync(action).GetTask();
    }

    public async Task InvokeAsync(Func<Task> action, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(action);
        cancellationToken.ThrowIfCancellationRequested();
        if (_dispatcher.CheckAccess())
        {
            await action().ConfigureAwait(true);
            return;
        }

        await _dispatcher.InvokeAsync(action).ConfigureAwait(false);
    }
}
