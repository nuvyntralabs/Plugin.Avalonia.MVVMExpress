# Plugin.Avalonia.MVVMExpress.Core

UI-framework-free MVVM primitives for **.NET**. No WPF or MAUI reference.

`ObservableModel`, `ViewModel` lifecycle, async commands, `AsyncState<T>`, `Outcome`, messaging, `INavigator`, `ICache`, and `IAuthState`.

```csharp
public sealed class HomeViewModel : ViewModel
{
  public AsyncState<IReadOnlyList<Product>> Products { get; } = new();
  public AsyncModelCommand RefreshCommand { get; }

  public HomeViewModel(ICatalog catalog)
  {
    RefreshCommand = new AsyncModelCommand(
      ct => Products.LoadAsync(token => catalog.ListAsync(token), ct));
  }
}
```

## Install

```bash
dotnet add package Plugin.Avalonia.MVVMExpress.Core
```

Target framework: `net10.0`. Version `1.0.1`.

```csharp
services.AddMvvmExpress(); // tests and shared libraries
```

WPF apps also add [Plugin.Avalonia.MVVMExpress](https://www.nuget.org/packages/Plugin.Avalonia.MVVMExpress) and call `UseAvaloniaMvvmExpress()`.

Do not call `MessageBox.Show` from a ViewModel. Use `IDialogs` / `INavigator`.

License: MIT. Niladri Padhy / MauiEssentials.
