using Avalonia.Controls;
using Plugin.Avalonia.MVVMExpress.Hosting;
using Plugin.Avalonia.MVVMExpress.Navigation;

namespace Plugin.Avalonia.MVVMExpress.Playground;

public partial class MainWindow : Window
{
    public MainWindow(INavigator navigator, IWindowNavigatorRegistry registry)
    {
        InitializeComponent();
        registry.Register(AvaloniaWindowContext.For(this), navigator);
    }
}
