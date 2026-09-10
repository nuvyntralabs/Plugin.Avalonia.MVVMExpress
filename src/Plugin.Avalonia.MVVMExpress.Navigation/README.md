# Plugin.Avalonia.MVVMExpress.Navigation

`AvaloniaFrameNavigator` maps ViewModel types to views on an Avalonia `Frame`. `INavigator` and `GuardedNavigator` live in Core.

```csharp
o.UseFrameNavigation((nav, _) => nav
    .Map<LoginViewModel, LoginPage>("login")
    .Map<HomeViewModel, HomePage>("home"));

await navigator.ResetAsync<HomeViewModel>();
```

Put a `Frame` named `NavigationHost` in `MainWindow`. There is no Shell host on Avalonia.

License: MIT.
