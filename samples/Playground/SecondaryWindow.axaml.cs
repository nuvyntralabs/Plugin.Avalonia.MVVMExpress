using Avalonia.Controls;
using Plugin.Avalonia.MVVMExpress.Hosting;

namespace Plugin.Avalonia.MVVMExpress.Playground;

public partial class SecondaryWindow : Window
{
    public SecondaryWindow()
    {
        InitializeComponent();
        AvaloniaWindowContext.For(this);
    }
}
