namespace Plugin.Avalonia.MVVMExpress.Templates.Tests;

public sealed class TemplateSmokeTests
{
    [Fact]
    public void Template_ContainsExpectedFiles()
    {
        var root = FindRepoRoot();
        Assert.True(File.Exists(Path.Combine(root, "templates", "avalonia-app", ".template.config", "template.json")));
        Assert.True(File.Exists(Path.Combine(root, "templates", "page", ".template.config", "template.json")));
        Assert.True(File.Exists(Path.Combine(root, "templates", "avalonia-app", "Directory.Build.props")));
    }

    private static string FindRepoRoot()
    {
        var current = new DirectoryInfo(AppContext.BaseDirectory);
        while (current is not null)
        {
            if (File.Exists(Path.Combine(current.FullName, "templates", "avalonia-app", ".template.config", "template.json")))
            {
                return current.FullName;
            }

            current = current.Parent;
        }

        throw new InvalidOperationException("Could not find the Plugin.Avalonia.MVVMExpress repository root.");
    }
}
