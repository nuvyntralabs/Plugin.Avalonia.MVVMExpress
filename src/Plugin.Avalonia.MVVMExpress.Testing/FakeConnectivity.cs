using Plugin.Avalonia.MVVMExpress.Connectivity;

namespace Plugin.Avalonia.MVVMExpress.Testing;

/// <summary>Mutable <see cref="IConnectivityProbe"/> for ViewModel tests.</summary>
public sealed class FakeConnectivity : IConnectivityProbe
{
    /// <inheritdoc />
    public bool IsOnline { get; set; } = true;
}
