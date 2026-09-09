using Result = Plugin.Avalonia.MVVMExpress.Outcome.Outcome;

namespace Plugin.Avalonia.MVVMExpress.Navigation;

/// <summary>
/// Maps an incoming URI onto <see cref="INavigator"/>. Production apps adapt Plugin.Maui.DeepLinks;
/// tests register an in-memory implementation.
/// </summary>
public interface IDeepLinkBridge
{
    /// <summary>Navigates to the route that matches <paramref name="uri"/>.</summary>
    Task<Result> NavigateAsync(string uri, INavigator navigator, CancellationToken cancellationToken = default);
}
