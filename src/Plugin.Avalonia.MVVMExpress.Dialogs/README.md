# Plugin.Avalonia.MVVMExpress.Dialogs

`AvaloniaDialogs` adapts `IDialogs` to a modal Avalonia `Window`. `AvaloniaNotifier` uses `WindowNotificationManager` and never wraps `Window.Content`.

```csharp
o.UseDialogs();
await Dialogs.AlertAsync("Saved", "The item is stored.");
await notifier.ToastAsync("Saved");
```

ViewModels must not open a `Window` directly. Use `IDialogs` / `INotifier`.

License: MIT.
