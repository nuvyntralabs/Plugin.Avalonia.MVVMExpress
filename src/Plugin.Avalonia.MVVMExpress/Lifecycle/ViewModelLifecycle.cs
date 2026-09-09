using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Plugin.Avalonia.MVVMExpress.ComponentModel;
using Plugin.Avalonia.MVVMExpress.Hosting;

namespace Plugin.Avalonia.MVVMExpress.Lifecycle;

/// <summary>Forwards visual-tree attach/detach to <see cref="IViewModel"/>.</summary>
public static class ViewModelLifecycle
{
    public static readonly AttachedProperty<bool> AutoProperty =
        AvaloniaProperty.RegisterAttached<Control, bool>("Auto", typeof(ViewModelLifecycle));

    private static readonly AttachedProperty<MvvmExpressOptions?> OptionsProperty =
        AvaloniaProperty.RegisterAttached<Control, MvvmExpressOptions?>("Options", typeof(ViewModelLifecycle));

    private static readonly AttachedProperty<bool> InitializedProperty =
        AvaloniaProperty.RegisterAttached<Control, bool>("Initialized", typeof(ViewModelLifecycle));

    static ViewModelLifecycle()
    {
        AutoProperty.Changed.AddClassHandler<Control>(OnAutoChanged);
    }

    public static bool GetAuto(AvaloniaObject element)
    {
        ArgumentNullException.ThrowIfNull(element);
        return element.GetValue(AutoProperty);
    }

    public static void SetAuto(AvaloniaObject element, bool value)
    {
        ArgumentNullException.ThrowIfNull(element);
        element.SetValue(AutoProperty, value);
    }

    public static void Attach(Control element, MvvmExpressOptions? options = null)
    {
        ArgumentNullException.ThrowIfNull(element);
        element.SetValue(OptionsProperty, options);
        SetAuto(element, true);
    }

    private static void OnAutoChanged(Control element, AvaloniaPropertyChangedEventArgs e)
    {
        element.Loaded -= OnLoaded;
        element.Unloaded -= OnUnloaded;
        if (e.NewValue is true)
        {
            element.Loaded += OnLoaded;
            element.Unloaded += OnUnloaded;
        }
    }

    private static async void OnLoaded(object? sender, RoutedEventArgs e)
    {
        if (sender is not Control element || element.DataContext is not IViewModel viewModel)
        {
            return;
        }

        try
        {
            if (!element.GetValue(InitializedProperty))
            {
                element.SetValue(InitializedProperty, true);
                await viewModel.InitializeAsync().ConfigureAwait(true);
            }

            await viewModel.OnAppearingAsync().ConfigureAwait(true);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
        }
    }

    private static async void OnUnloaded(object? sender, RoutedEventArgs e)
    {
        if (sender is not Control element || element.DataContext is not IViewModel viewModel)
        {
            return;
        }

        try
        {
            await viewModel.OnDisappearingAsync().ConfigureAwait(true);
            if (element.GetValue(OptionsProperty) is MvvmExpressOptions { CancelOperationsOnDisappear: true })
            {
                viewModel.CancelPendingOperations();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
        }
    }
}
