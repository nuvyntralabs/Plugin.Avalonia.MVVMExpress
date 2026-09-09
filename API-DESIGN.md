# Plugin.Avalonia.MVVMExpress Public API Design

**1.0.0** contract. Namespaces start with `Plugin.Avalonia.MVVMExpress`. Core shapes match Maui 1.3 / WPF 1.0 so a ViewModel ports with a namespace swap. This is **not** a type-forward of sibling families.

## Hosting

```csharp
services.AddMvvmExpress();
services.AddAvaloniaMvvmExpress(configure);
builder.UseAvaloniaMvvmExpress(configure);
options.UseFrameNavigation(configure);
options.UseDialogs();
options.UseAuth<TChallenge>();
```

## Core (shipped)

`ObservableModel`, `ViewModel`, `PageViewModel`, `ModelCommand`, `AsyncModelCommand`, `AsyncState<T>`, `Outcome`, `IMessageHub`, `INavigator`, `IPageNavigator`, `InMemoryNavigator`, `GuardedNavigator`, `IDialogs`, `INotifier`, `IMainThread`, `IWindowContext`, `FormViewModel`, `SectionHostViewModel`, `IAuthState`, `ICache`, `IConnectivityProbe`.

## Hosts (shipped)

- `AvaloniaDispatcherMainThread`
- `AvaloniaWindowContext.For(Window)` / `.Current`
- `ViewModelLifecycle.SetAuto`
- `AvaloniaFrameNavigator.Map<TViewModel, TView>`
- `AvaloniaDialogs` / `AvaloniaNotifier` / `AvaloniaToastPresenter`
- `AvaloniaFormViewModel` (`INotifyDataErrorInfo`)

## Out of 1.0

Shell, `UseDeepLinks`, `UseSecureSessionAuth`, source generators, Reactive, CommunityToolkit compatibility, `NavigateForResultAsync`. IDE extensions are Phase 3.
