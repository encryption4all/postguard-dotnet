namespace E4A.PostGuard.Tests;

/// <summary>
/// The workflows feed <c>.github/pg-ffi-version</c> straight into
/// <c>gh release download &lt;tag&gt;</c>. An empty or malformed bump either breaks
/// the build or, worse, makes <c>gh</c> fall back to the newest release.
/// </summary>
public class PgFfiVersionTests
{
    [Fact]
    public void PinnedVersionFile_HoldsExactlyOneReleaseTag()
    {
        var path = Path.Combine(RepoRoot(), ".github", "pg-ffi-version");
        Assert.True(File.Exists(path), $"pinned pg-ffi version file not found: {path}");

        var lines = File.ReadAllLines(path).Where(line => line.Trim().Length > 0).ToArray();

        Assert.Single(lines);
        Assert.Matches(@"^pg-ffi-v\d+\.\d+\.\d+$", lines[0]);
    }

    private static string RepoRoot()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir is not null && !File.Exists(Path.Combine(dir.FullName, "E4A.PostGuard.slnx")))
        {
            dir = dir.Parent;
        }

        Assert.NotNull(dir);
        return dir!.FullName;
    }
}
