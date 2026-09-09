# Getting started

Scaffold an app:

```bash
dotnet new install Plugin.Avalonia.MVVMExpress.Templates
dotnet new avalonia-mvvmexpress -n MyApp
```

Or add the packages to an existing Avalonia project:

```bash
dotnet add package Plugin.Avalonia.MVVMExpress.Core
dotnet add package Plugin.Avalonia.MVVMExpress
dotnet add package Plugin.Avalonia.MVVMExpress.Navigation
dotnet add package Plugin.Avalonia.MVVMExpress.Dialogs
```

```csharp
var builder = Host.CreateApplicationBuilder();
builder.Services.AddSingleton<IAuthState, DemoAuthState>();
builder.Services.UseAvaloniaMvvmExpress(o => o
    .UseFrameNavigation((nav, _) => nav
        .Map<LoginViewModel, LoginPage>("login")
        .Map<HomeViewModel, HomePage>("home"))
    .UseDialogs()
    .UseAuth<LoginViewModel>());
```

Add a `Frame` named `NavigationHost` to the main window. Toasts must not replace `Window.Content`.

Demo credentials in Playground: `demo@mvvmexpress.dev` / `secret`.

After sign-in, `ResetAsync<HomeViewModel>()` replaces the journal so Back cannot return to login.
