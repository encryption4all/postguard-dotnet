using E4A.PostGuard.Models;

namespace E4A.PostGuard;

public class PostGuard
{
    private readonly PostGuardConfig _config;

    public PostGuard(PostGuardConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);
        config.Validate();
        _config = config;
    }

    /// <summary>
    /// Builder for signing methods.
    /// </summary>
    public SignBuilders Sign { get; } = new();

    /// <summary>
    /// Builder for recipients.
    /// </summary>
    public RecipientBuilders Recipient { get; } = new();

    /// <summary>
    /// Create a lazy encryption builder. No work is done until a terminal
    /// method (<see cref="Sealed.UploadAsync"/> or <see cref="Sealed.ToBytesAsync"/>)
    /// is called.
    /// </summary>
    public Sealed Encrypt(EncryptInput input)
    {
        return new Sealed(_config, input);
    }
}

public class SignBuilders
{
    public ApiKeySign ApiKey(string apiKey) => new(apiKey);
}

public class RecipientBuilders
{
    public RecipientBuilder Email(string email) => new(email, RecipientBaseType.Email);
    public RecipientBuilder EmailDomain(string email) => new(email, RecipientBaseType.EmailDomain);
}
