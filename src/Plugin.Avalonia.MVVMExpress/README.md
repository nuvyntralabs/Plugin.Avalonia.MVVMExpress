# Plugin.Avalonia.MVVMExpress

Avalonia host for MVVMExpress. Target: `net10.0`. DI, dispatcher, window context, and Loaded/Unloaded lifecycle.

Depends on [Plugin.Avalonia.MVVMExpress.Core](https://www.nuget.org/packages/Plugin.Avalonia.MVVMExpress.Core).

```csharp
var builder = Host.CreateApplicationBuilder();
builder.Services.UseAvaloniaMvvmExpress(o => o
    .UseFrameNavigation((nav, _) => nav
        .Map<HomeViewModel, HomePage>("home"))
    .UseDialogs()
    .UseAuth<LoginViewModel>());
```

`UseAvaloniaMvvmExpress` calls `AddMvvmExpress()`, replaces `IMainThread` with `AvaloniaDispatcherMainThread`, and maps `IWindowContext` to `AvaloniaWindowContext`. `UseFrameNavigation` / `UseDialogs` live in the Navigation and Dialogs packages.

## Install

```bash
dotnet add package Plugin.Avalonia.MVVMExpress.Core
dotnet add package Plugin.Avalonia.MVVMExpress
```

Version `1.0.1`. Shared / test code can stay on Core + `AddMvvmExpress()` without this package.

License: MIT. Niladri Padhy / MauiEssentials.
