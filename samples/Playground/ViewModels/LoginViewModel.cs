using Plugin.Avalonia.MVVMExpress.Auth;
using Plugin.Avalonia.MVVMExpress.ComponentModel;
using Plugin.Avalonia.MVVMExpress.Dialogs;
using Plugin.Avalonia.MVVMExpress.Input;
using Plugin.Avalonia.MVVMExpress.Navigation;

namespace Plugin.Avalonia.MVVMExpress.Playground.ViewModels;

public sealed class LoginViewModel : PageViewModel
{
    private readonly IAuthState _auth;
    private string _email = "demo@mvvmexpress.dev";
    private string _password = "secret";

    public LoginViewModel(INavigator navigator, IDialogs dialogs, IAuthState auth)
        : base(navigator, dialogs)
    {
        _auth = auth;
        SignInCommand = new AsyncModelCommand(SignInAsync, () => !string.IsNullOrWhiteSpace(Email));
    }

    public string Email
    {
        get => _email;
        set
        {
            if (SetProperty(ref _email, value))
            {
                SignInCommand.NotifyCanExecuteChanged();
            }
        }
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public AsyncModelCommand SignInCommand { get; }

    private async Task SignInAsync(CancellationToken cancellationToken)
    {
        var result = await _auth.SignInAsync(Email, Password, cancellationToken).ConfigureAwait(true);
        if (!result.IsSuccess)
        {
            await Dialogs!.ErrorAsync(result.Error!, cancellationToken).ConfigureAwait(true);
            return;
        }

        await Navigator!.ResetAsync<HomeViewModel>(cancellationToken).ConfigureAwait(true);
    }
}
