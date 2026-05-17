using System.Text.Json;
using E4A.PostGuard.Api;
using E4A.PostGuard.Models;
using E4A.PostGuard.Zip;

namespace E4A.PostGuard.Crypto;

internal static class SealPipeline
{
    private const string EmailAttributeType = "pbdf.sidn-pbdf.email.email";
    private const string DomainAttributeType = "pbdf.sidn-pbdf.email.domain";

    /// <summary>
    /// Seal files and upload to Cryptify.
    /// </summary>
    public static async Task<UploadResult> SealAndUploadAsync(
        PostGuardConfig config,
        HttpClient http,
        EncryptInput input,
        UploadOptions? uploadOptions,
        CancellationToken ct = default)
    {
        var sealedBytes = await SealAsync(config, http, input, ct);

        var cryptify = new CryptifyClient(http, config.CryptifyUrl);
        var uuid = await cryptify.UploadAsync(
            sealedBytes, input.Recipients, uploadOptions?.Notify, ct);

        return new UploadResult(uuid);
    }

    /// <summary>
    /// Seal files and return sealed bytes (no upload).
    /// </summary>
    public static async Task<byte[]> SealAsync(
        PostGuardConfig config,
        HttpClient http,
        EncryptInput input,
        CancellationToken ct = default)
    {
        var apiKey = input.Sign is ApiKeySign ak
            ? ak.ApiKey
            : throw new ArgumentException("Only ApiKey signing is supported");

        var pkg = new PkgClient(http, config.PkgUrl);

        // Fetch MPK and signing keys in parallel
        var mpkTask = pkg.FetchMpkJsonAsync(ct);
        var signKeysTask = pkg.FetchSigningKeysAsync(apiKey, ct);
        await Task.WhenAll(mpkTask, signKeysTask);

        var mpkJson = mpkTask.Result;
        var (pubSignKeyJson, privSignKeyJson) = signKeysTask.Result;

        // Build encryption policy
        var policyJson = BuildPolicyJson(input.Recipients);

        // Create ZIP of files
        var zipBytes = ZipHelper.CreateZip(input.Files);

        // Seal via native library
        return Native.Seal(mpkJson, policyJson, pubSignKeyJson, privSignKeyJson, zipBytes);
    }

    internal static string BuildPolicyJson(IReadOnlyList<RecipientBuilder> recipients)
    {
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var policy = new Dictionary<string, object>();

        foreach (var r in recipients)
        {
            var con = new List<object>();

            if (r.BaseType == "email")
            {
                con.Add(new { t = EmailAttributeType, v = r.Email });
            }
            else if (r.BaseType == "emailDomain")
            {
                // Extract domain from email
                var domain = r.Email.Contains('@') ? r.Email.Split('@')[1] : r.Email;
                con.Add(new { t = DomainAttributeType, v = domain });
            }

            foreach (var (type, value) in r.Extras)
            {
                con.Add(new { t = type, v = value });
            }

            policy[r.Email] = new { ts = timestamp, con };
        }

        return JsonSerializer.Serialize(policy);
    }
}
