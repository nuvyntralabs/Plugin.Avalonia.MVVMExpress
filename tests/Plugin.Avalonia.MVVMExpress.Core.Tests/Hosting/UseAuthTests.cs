using Microsoft.Extensions.DependencyInjection;
using Plugin.Avalonia.MVVMExpress.Auth;
using Plugin.Avalonia.MVVMExpress.ComponentModel;
using Plugin.Avalonia.MVVMExpress.Hosting;
using Plugin.Avalonia.MVVMExpress.Navigation;

namespace Plugin.Avalonia.MVVMExpress.Core.Tests.Hosting;

public sealed class UseAuthTests
{
    [Fact]
    public void AddAuth_WrapsNavigator_WithGuardedNavigator()
    {
        using var provider = new ServiceCollection()
            .AddMvvmExpress()
            .AddSingleton<IAuthState, MemoryAuth>()
            .AddAuth<LoginVm>()
            .BuildServiceProvider();

        var navigator = provider.GetRequiredService<INavigator>();
        Assert.IsType<GuardedNavigator>(navigator);
        Assert.Same(navigator, provider.GetRequiredService<IPageNavigator>());
    }

    [Fact]
    public async Task AddAuth_OpensChallenge_ThenResumes()
    {
        var services = new ServiceCollection()
            .AddMvvmExpress()
            .AddSingleton<IAuthState, MemoryAuth>()
            .AddSingleton<INavigationAuthPolicy>(new NavigationAuthPolicy(authRequired: [typeof(HomeVm)]))
            .AddAuth<LoginVm>();
        using var provider = services.BuildServiceProvider();
        var inner = provider.GetRequiredService<InMemoryNavigator>()
            .Map<LoginVm>("login")
            .Map<HomeVm>("home");
        var navigator = provider.GetRequiredService<INavigator>();

        var blocked = await navigator.NavigateToAsync<HomeVm>();
        Assert.True(blocked.IsSuccess);
        Assert.Equal(typeof(LoginVm), inner.Current);

        await provider.GetRequiredService<IAuthState>().SignInAsync("ada", "secret");
        await Task.Delay(20);
        Assert.Equal(typeof(HomeVm), inner.Current);
    }

    [Fact]
    public void AddAuth_WithoutAuthState_ThrowsClearMessage()
    {
        using var provider = new ServiceCollection()
            .AddMvvmExpress()
            .AddAuth<LoginVm>()
            .BuildServiceProvider();

        var ex = Assert.Throws<InvalidOperationException>(() => provider.GetRequiredService<INavigator>());
        Assert.Contains("IAuthState", ex.Message, StringComparison.Ordinal);
    }

    private sealed class HomeVm : ViewModel;

    private sealed class LoginVm : ViewModel;

    private sealed class MemoryAuth : IAuthState
    {
        public bool IsAuthenticated { get; private set; }

        public string? UserName => IsAuthenticated ? "ada" : null;

        public event EventHandler? Changed;

        public Task<Plugin.Avalonia.MVVMExpress.Outcome.Outcome> SignInAsync(
            string userName,
            string password,
            CancellationToken cancellationToken = default)
        {
            IsAuthenticated = true;
            Changed?.Invoke(this, EventArgs.Empty);
            return Task.FromResult(Plugin.Avalonia.MVVMExpress.Outcome.Outcome.Success());
        }

        public Task SignOutAsync(CancellationToken cancellationToken = default)
        {
            IsAuthenticated = false;
            Changed?.Invoke(this, EventArgs.Empty);
            return Task.CompletedTask;
        }
    }
}
