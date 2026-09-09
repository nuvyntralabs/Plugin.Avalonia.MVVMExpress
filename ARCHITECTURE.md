# Plugin.Avalonia.MVVMExpress Architecture

Independent Avalonia MVVM family. **1.0.0.** Aligned with the Plugin.Maui.MVVMExpress 1.3 / Plugin.Wpf.MVVMExpress 1.0 Core contract, but **not** a package reference to those families.

## Principles

1. Core is UI-framework-free (`net10.0`).
2. Host / Navigation / Dialogs target `net10.0`.
3. ViewModels depend on `INavigator`, `IDialogs`, `IMainThread`.
4. One navigator per `Window` (`IWindowContext` / `WindowNavigatorRegistry`).
5. No sibling MauiEssentials `PackageReference`.
6. No Shell. Frame + `SectionHostViewModel` cover stack and in-place tabs.

## Packages

- `Plugin.Avalonia.MVVMExpress.Core` — ViewModels, commands, state, navigation abstractions
- `Plugin.Avalonia.MVVMExpress` — `UseAvaloniaMvvmExpress`, `AvaloniaDispatcherMainThread`, lifecycle
- `Plugin.Avalonia.MVVMExpress.Navigation` — `AvaloniaFrameNavigator`
- `Plugin.Avalonia.MVVMExpress.Dialogs` — `AvaloniaDialogs`, overlay toasts
- `Plugin.Avalonia.MVVMExpress.Validation` / `.Pagination` / `.Testing`
- `Plugin.Avalonia.MVVMExpress.Templates` — `dotnet new avalonia-mvvmexpress`

## Host

```csharp
builder.Services.UseAvaloniaMvvmExpress(o => o
    .UseFrameNavigation((nav, _) => nav.Map<HomeViewModel, HomePage>("home"))
    .UseDialogs()
    .UseAuth<LoginViewModel>());
```

Put a `Frame` named `NavigationHost` in the window. Toasts must not replace `Window.Content`.
