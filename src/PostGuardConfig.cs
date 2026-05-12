namespace E4A.PostGuard;

public class PostGuardConfig
{
    public required string PkgUrl { get; init; }
    public required string CryptifyUrl { get; init; }
    public Dictionary<string, string>? Headers { get; init; }

    /// <summary>
    /// Opt-in escape hatch to allow non-https URLs (e.g. http://localhost) for
    /// local development and testing. Defaults to false: the SDK refuses to
    /// send API keys and signing keys over plaintext connections.
    /// </summary>
    public bool AllowInsecureUrls { get; init; }

    internal void Validate()
    {
        if (AllowInsecureUrls)
        {
            return;
        }

        RequireHttps(PkgUrl, nameof(PkgUrl));
        RequireHttps(CryptifyUrl, nameof(CryptifyUrl));
    }

    private static void RequireHttps(string url, string name)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) ||
            uri.Scheme != Uri.UriSchemeHttps)
        {
            throw new ArgumentException(
                $"{name} must be an absolute https:// URL (got '{url}'). " +
                "Set AllowInsecureUrls = true to opt in to plaintext URLs for local testing.",
                name);
        }
    }
}
