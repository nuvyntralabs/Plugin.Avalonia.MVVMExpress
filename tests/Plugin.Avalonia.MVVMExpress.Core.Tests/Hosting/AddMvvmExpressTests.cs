using Microsoft.Extensions.DependencyInjection;
using Plugin.Avalonia.MVVMExpress.Caching;
using Plugin.Avalonia.MVVMExpress.Composition;
using Plugin.Avalonia.MVVMExpress.Connectivity;
using Plugin.Avalonia.MVVMExpress.Dialogs;
using Plugin.Avalonia.MVVMExpress.Files;
using Plugin.Avalonia.MVVMExpress.Flags;
using Plugin.Avalonia.MVVMExpress.Hosting;
using Plugin.Avalonia.MVVMExpress.Media;
using Plugin.Avalonia.MVVMExpress.Messaging;
using Plugin.Avalonia.MVVMExpress.Navigation;
using Plugin.Avalonia.MVVMExpress.Operations;
using Plugin.Avalonia.MVVMExpress.Permissions;
using Plugin.Avalonia.MVVMExpress.Threading;

namespace Plugin.Avalonia.MVVMExpress.Core.Tests.Hosting;

public sealed class AddMvvmExpressTests
{
    [Fact]
    public void AddMvvmExpress_RegistersCoreSingletons()
    {
        using var provider = new ServiceCollection().AddMvvmExpress().BuildServiceProvider();
        Assert.IsType<MessageHub>(provider.GetRequiredService<IMessageHub>());
        Assert.IsType<MemoryCache>(provider.GetRequiredService<ICache>());
        Assert.IsType<InMemoryConnectivityProbe>(provider.GetRequiredService<IConnectivityProbe>());
        var navigator = Assert.IsType<InMemoryNavigator>(provider.GetRequiredService<INavigator>());
        Assert.Same(navigator, provider.GetRequiredService<IPageNavigator>());
        Assert.Same(ImmediateMainThread.Instance, provider.GetRequiredService<IMainThread>());
        Assert.IsType<NullDialogs>(provider.GetRequiredService<IDialogs>());
        Assert.Same(NullDialogs.Instance, provider.GetRequiredService<INotifier>());
        Assert.Equal("default", provider.GetRequiredService<IWindowContext>().WindowId);
        Assert.IsType<WindowNavigatorRegistry>(provider.GetRequiredService<IWindowNavigatorRegistry>());
        Assert.IsType<CachedFetcher>(provider.GetRequiredService<ICachedFetcher>());
        Assert.IsType<OperationExecutor>(provider.GetRequiredService<IOperationExecutor>());
        Assert.IsType<ServiceViewModelScopeFactory>(provider.GetRequiredService<IViewModelScopeFactory>());
        Assert.IsType<MemoryFeatureSwitch>(provider.GetRequiredService<IFeatureSwitch>());
        Assert.Same(AllowAllPermissionGate.Instance, provider.GetRequiredService<IPermissionGate>());
        Assert.IsType<MemoryFileStore>(provider.GetRequiredService<IFileStore>());
        Assert.Same(NullMediaPicker.Instance, provider.GetRequiredService<IMediaPicker>());
    }
}
