using System.Collections;
using System.ComponentModel;
using Plugin.Avalonia.MVVMExpress.Busy;
using Plugin.Avalonia.MVVMExpress.Dialogs;
using Plugin.Avalonia.MVVMExpress.Errors;
using Plugin.Avalonia.MVVMExpress.Navigation;
using Plugin.Avalonia.MVVMExpress.Outcome;
using Plugin.Avalonia.MVVMExpress.Threading;

namespace Plugin.Avalonia.MVVMExpress.Forms;

/// <summary><see cref="FormViewModel"/> that implements <see cref="INotifyDataErrorInfo"/>.</summary>
public abstract class AvaloniaFormViewModel : FormViewModel, INotifyDataErrorInfo
{
    private IReadOnlyList<ValidationMessage> _errors = [];

    protected AvaloniaFormViewModel(
        INavigator? navigator = null,
        IDialogs? dialogs = null,
        IErrorSink? errors = null,
        IBusyGate? busy = null,
        IMainThread? mainThread = null)
        : base(navigator, dialogs, errors, busy, mainThread)
    {
    }

    public bool HasErrors => _errors.Count > 0;

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public IEnumerable GetErrors(string? propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
        {
            return _errors.Select(static m => m.Message);
        }

        return _errors
            .Where(m => string.Equals(m.PropertyName, propertyName, StringComparison.Ordinal))
            .Select(static m => m.Message);
    }

    protected async Task ValidateFormAsync(CancellationToken cancellationToken = default)
    {
        _errors = await CollectErrorsAsync(cancellationToken).ConfigureAwait(true);
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(null));
    }

    protected virtual Task<IReadOnlyList<ValidationMessage>> CollectErrorsAsync(CancellationToken cancellationToken)
        => Task.FromResult<IReadOnlyList<ValidationMessage>>([]);
}
