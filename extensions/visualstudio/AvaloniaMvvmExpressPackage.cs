using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace NuvyntraLabs.AvaloniaMVVMExpress.VisualStudio;

[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
[InstalledProductRegistration("Avalonia MVVMExpress", "Create Avalonia MVVMExpress apps and pages from Plugin.Avalonia.MVVMExpress.Templates.", "1.0.0")]
[ProvideMenuResource("Menus.ctmenu", 1)]
[ProvideAutoLoad(UIContextGuids80.NoSolution, PackageAutoLoadFlags.BackgroundLoad)]
[ProvideAutoLoad(UIContextGuids80.SolutionExists, PackageAutoLoadFlags.BackgroundLoad)]
[Guid(AvaloniaMvvmExpressPackage.PackageGuidString)]
public sealed class AvaloniaMvvmExpressPackage : AsyncPackage
{
    public const string PackageGuidString = "e3f4a5b6-8b4d-4e91-9c2a-6f0d8e1b7a44";

    protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
    {
        await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
        await Commands.InitializeAsync(this).ConfigureAwait(true);
        try
        {
            await DotnetTemplates.EnsureInstalledAsync(cancellationToken).ConfigureAwait(true);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            ActivityLog.LogWarning("Avalonia MVVMExpress", ex.Message);
        }
    }
}
