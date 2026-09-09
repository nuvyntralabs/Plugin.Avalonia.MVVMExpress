namespace Plugin.Avalonia.MVVMExpress.Core.Tests;

public sealed class DesignSkeletonTests
{
    [Fact]
    public void Core_ExposesProductIdentity()
    {
        Assert.Equal("MVVMExpress", AssemblyMarker.Product);
        Assert.Equal("Plugin.Avalonia.MVVMExpress", AssemblyMarker.PackagePrefix);
    }
}
