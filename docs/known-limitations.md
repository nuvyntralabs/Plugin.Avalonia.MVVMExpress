# Known limitations — Plugin.Avalonia.MVVMExpress 1.0

- Desktop first (Windows, macOS, Linux). iOS / Android / WASM are out of 1.0.
- Avalonia 11 has no stock `Frame`. Use `Plugin.Avalonia.MVVMExpress.Controls.Frame` named `NavigationHost`. Core `NavigationStack` is the source of truth.
- Dialogs are modal Avalonia `Window`s, not Win32 `MessageBox`.
- Toasts use `WindowNotificationManager` and never replace `Window.Content`.
- No ReactiveUI or FluentAvalonia dependency.
- Source generators, Reactive, CommunityToolkit compatibility, `UseDeepLinks`, and `UseSecureSessionAuth` are out of 1.0.
