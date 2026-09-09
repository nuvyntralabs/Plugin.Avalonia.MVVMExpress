# Changelog

## 0.1.0-preview

- Independent Avalonia family (no sibling MVVMExpress package references).
- Core, Validation, Pagination, Testing ported to `Plugin.Avalonia.MVVMExpress` namespaces.
- Host: `UseAvaloniaMvvmExpress`, `AvaloniaDispatcherMainThread`, `AvaloniaWindowContext`, lifecycle.
- Navigation: `AvaloniaFrameNavigator`, `UseAuth<TChallenge>()`.
- Dialogs: `AvaloniaDialogs` and overlay toasts.
- Playground sample: login replace-root, list, form, toast, second window.
- Templates: `dotnet new avalonia-mvvmexpress` / `avalonia-mvvmexpress-page`.
