# Plugin.Avalonia.MVVMExpress

Modular MVVM for **Avalonia** on .NET 10: ViewModels, async commands, Frame navigation, dialogs, validation, pagination.

**Product:** MVVMExpress (Avalonia family)
**Package prefix:** `Plugin.Avalonia.MVVMExpress`
**Status:** `1.0.1`
**This is not** Plugin.Maui.MVVMExpress, Plugin.Wpf.MVVMExpress, or the other desktop families. Independent port — no PackageReference to those packages.

[![NuGet](https://img.shields.io/nuget/v/Plugin.Avalonia.MVVMExpress.Core.svg?label=NuGet)](https://www.nuget.org/packages/Plugin.Avalonia.MVVMExpress.Core)

Author: [Niladri Prasad Padhy](https://github.com/NiladriPadhy) · Catalog: [MauiEssentials](https://github.com/nuvyntralabs/MauiEssentials) · License: MIT

## Install

```bash
dotnet add package Plugin.Avalonia.MVVMExpress.Core
dotnet add package Plugin.Avalonia.MVVMExpress
dotnet add package Plugin.Avalonia.MVVMExpress.Navigation
dotnet add package Plugin.Avalonia.MVVMExpress.Dialogs
```

```csharp
builder.Services.UseAvaloniaMvvmExpress(o => o
    .UseFrameNavigation((nav, _) => nav
        .Map<HomeViewModel, HomePage>("home"))
    .UseDialogs()
    .UseAuth<LoginViewModel>());
```

There is no Shell host. Use `Plugin.Avalonia.MVVMExpress.Controls.Frame` named `NavigationHost`.

## Templates and IDE extensions

| Host | How |
| --- | --- |
| CLI | `Plugin.Avalonia.MVVMExpress.Templates` |
| Visual Studio Code | Search Avalonia MVVMExpress |
| Visual Studio 2022+ | Search Avalonia MVVMExpress |

Extensions install the NuGet template pack and run `dotnet new`. Marketplace publish is manual from the `ide-extensions` workflow artifact.

## Templates

```bash
dotnet new install Plugin.Avalonia.MVVMExpress.Templates
dotnet new avalonia-mvvmexpress -n MyApp
dotnet new avalonia-mvvmexpress-page -n Catalog --namespace MyApp
```

## Packages

| Package | TFM | Role |
| --- | --- | --- |
| `Plugin.Avalonia.MVVMExpress.Core` | `net10.0` | ViewModels, commands, state, abstractions |
| `Plugin.Avalonia.MVVMExpress` | `net10.0` | Host, dispatcher, lifecycle |
| `Plugin.Avalonia.MVVMExpress.Navigation` | `net10.0` | `AvaloniaFrameNavigator` |
| `Plugin.Avalonia.MVVMExpress.Dialogs` | `net10.0` | `AvaloniaDialogs` + overlay toast |
| `Plugin.Avalonia.MVVMExpress.Validation` | `net10.0` | DataAnnotations |
| `Plugin.Avalonia.MVVMExpress.Pagination` | `net10.0` | Lists / search |
| `Plugin.Avalonia.MVVMExpress.Testing` | `net10.0` | Fakes / leak probe |
| `Plugin.Avalonia.MVVMExpress.Templates` | `net10.0` | `dotnet new avalonia-mvvmexpress` |

Playground: `samples/Playground`. Docs: [getting started](docs/getting-started.md) · [API design](API-DESIGN.md) · [parity](API-PARITY.md).

Usual alternative: [CommunityToolkit.Mvvm](https://www.nuget.org/packages/CommunityToolkit.Mvvm).
