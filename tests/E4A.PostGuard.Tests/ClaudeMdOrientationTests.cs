namespace E4A.PostGuard.Tests;

/// <summary>
/// The root <c>CLAUDE.md</c> is orientation: what this repo is, the position it takes in the
/// PostGuard family, and which sibling repos a change here touches. It had become a 3,525-byte
/// list of everything an agent had ever noticed in the repo, and
/// <see href="https://github.com/encryption4all/dobby-code/issues/696">dobby-code#696</see> cut it
/// back. Nothing may refill it: documentation goes to
/// <c>docs.postguard.eu/repos/postguard-dotnet</c>, a durable check goes to the rule bundle, and a
/// repo invariant goes to the test that enforces it.
/// </summary>
public class ClaudeMdOrientationTests
{
    /// <summary>
    /// 4,000 B against an orientation file that lands near 2,900 B: room to add a section or
    /// reword a line, not room for a second corpus. The number is also the gate from
    /// <see href="https://github.com/encryption4all/dobby-code/issues/482">dobby-code#482</see> that
    /// decides whether a container working this repo gets its cwd pointed at the clone, so raising
    /// it costs more than taste.
    /// </summary>
    private const int MaxBytes = 4_000;

    /// <summary>
    /// The headings the cut removed. The byte cap alone would let any one of them return in a
    /// slimmer form, and each is a whole category the file is no longer for: a container
    /// environment quirk, a build mechanism, an API walkthrough, three invariants that already
    /// have tests of their own (<see cref="PgFfiVersionTests"/>,
    /// <see cref="PublicApiTrackingTests"/>, <see cref="CryptifyContentRangeTests"/>,
    /// <see cref="ZipHelperTests"/>).
    /// </summary>
    private static readonly string[] CutSections =
    [
        "Workspace limitation",
        "Pinned pg-ffi native binaries",
        "Tracked public API surface",
        "API layout",
        "Chunked upload range header",
        "Zip Slip sanitization",
    ];

    [Fact]
    public void ClaudeMd_StaysOrientationSized()
    {
        var bytes = new FileInfo(ClaudeMdPath()).Length;

        Assert.True(
            bytes <= MaxBytes,
            $"CLAUDE.md is {bytes} B, over the {MaxBytes} B cap. This file is ORIENTATION — what this "
                + "repo is, where it sits in the PostGuard family, and which siblings a change here "
                + "touches. Documentation belongs at docs.postguard.eu/repos/postguard-dotnet; a durable "
                + "check belongs in the rule bundle (POST /rules/temporary, at most 600 bytes, delivered "
                + "to the next container at ~/dobby-rules.md); a repo invariant belongs in the test that "
                + "enforces it. See dobby-code#696.");
    }

    [Fact]
    public void ClaudeMd_HasNoHeadingFromTheCutCorpus()
    {
        var headings = File.ReadLines(ClaudeMdPath())
            .Where(line => line.StartsWith("##", StringComparison.Ordinal))
            .Select(line => line.TrimStart('#').Trim())
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var returned = CutSections.Where(headings.Contains).ToArray();

        Assert.True(
            returned.Length == 0,
            $"CLAUDE.md carries a heading from the cut corpus again: {string.Join(", ", returned)}. "
                + "That content is documentation, a binding rule or a test's doc comment now, not this "
                + "file. See dobby-code#696.");
    }

    private static string ClaudeMdPath() => Path.Combine(RepoRoot(), "CLAUDE.md");

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
