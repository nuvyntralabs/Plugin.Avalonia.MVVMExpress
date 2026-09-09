using Avalonia.Controls;

namespace Plugin.Avalonia.MVVMExpress.Controls;

/// <summary>
/// View host for stack navigation. Avalonia 11 has no stock <c>Frame</c>;
/// this <see cref="ContentControl"/> is the host. Core <c>NavigationStack</c> is the source of truth.
/// Put one in the window named <c>NavigationHost</c>.
/// </summary>
public class Frame : ContentControl
{
}
