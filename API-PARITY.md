# API parity — Maui 1.3 / WPF 1.0 → Avalonia

| Surface | Status |
| --- | --- |
| Core ViewModel / commands / AsyncState / Outcome / messenger | Port |
| `INavigator` method surface | Port |
| `UseAuth<TChallenge>` / `GuardedNavigator` | Port |
| Validation / Pagination / Testing | Port |
| `UseWpfMvvmExpress` | Adapt → `UseAvaloniaMvvmExpress` |
| `DispatcherMainThread` | Adapt → `AvaloniaDispatcherMainThread` |
| Lifecycle | Adapt → host attach/detach |
| `WpfFrameNavigator` | Adapt → `AvaloniaFrameNavigator` |
| Dialogs / toasts | Adapt → `AvaloniaDialogs` / overlay |
| `AvaloniaFormViewModel` | Adapt → `INotifyDataErrorInfo` |
| Shell / `UseShell` | Skip |
| `UseDeepLinks` / `UseSecureSessionAuth` | Skip |
| Source generators | Skip |
| Templates | Port — `avalonia-mvvmexpress` / `avalonia-mvvmexpress-page` |
| IDE extensions | Skip (Phase 3) |
