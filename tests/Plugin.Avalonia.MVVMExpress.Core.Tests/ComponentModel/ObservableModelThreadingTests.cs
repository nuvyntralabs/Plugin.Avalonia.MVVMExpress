using Plugin.Avalonia.MVVMExpress.Core.Tests.Support;
using Plugin.Avalonia.MVVMExpress.Testing;
using Plugin.Avalonia.MVVMExpress.Threading;

namespace Plugin.Avalonia.MVVMExpress.Core.Tests.ComponentModel;

public sealed class ObservableModelThreadingTests
{
    [Fact]
    public void SetProperty_OffThread_HopsPropertyChanged()
    {
        var main = new RecordingMainThread { IsMainThread = false };
        using var scope = NotificationMarshaller.UseScope(main);
        var vm = new ProbeViewModel();
        var offThread = 0;
        vm.PropertyChanged += (_, _) =>
        {
            if (!main.IsInvoking)
            {
                offThread++;
            }
        };

        vm.Name = "hop";
        Assert.Equal(0, offThread);
        Assert.True(main.InvokeCount >= 1);
        Assert.Equal("hop", vm.Name);
    }
}
