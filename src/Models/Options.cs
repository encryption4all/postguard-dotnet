namespace E4A.PostGuard.Models;

public record PgFile(string Name, Stream Content);

public class EncryptInput
{
    public required IReadOnlyList<PgFile> Files { get; init; }
    public required IReadOnlyList<RecipientBuilder> Recipients { get; init; }
    public required SignMethod Sign { get; init; }
}

public class UploadOptions
{
    public NotifyOptions? Notify { get; init; }
}

public class NotifyOptions
{
    public string? Message { get; init; }
    public string Language { get; init; } = "EN";
    public bool ConfirmToSender { get; init; } = false;
}
