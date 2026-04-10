namespace E4A.PostGuard;

public class PostGuardConfig
{
    public required string PkgUrl { get; init; }
    public required string CryptifyUrl { get; init; }
    public Dictionary<string, string>? Headers { get; init; }
}
