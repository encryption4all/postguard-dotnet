using E4A.PostGuard.Exceptions;

namespace E4A.PostGuard.Tests;

public class NetworkExceptionTests
{
    [Fact]
    public void Ctor_PopulatesUrlProperty()
    {
        var ex = new NetworkException(401, "Unauthorized", "https://pkg.postguard.eu/v2/irma/sign/key");

        Assert.Equal(401, ex.StatusCode);
        Assert.Equal("Unauthorized", ex.Body);
        Assert.Equal("https://pkg.postguard.eu/v2/irma/sign/key", ex.Url);
    }

    [Fact]
    public void Ctor_IncludesUrlAndStatusInMessage()
    {
        var ex = new NetworkException(500, "boom", "https://cryptify.postguard.eu/fileupload/init");

        Assert.Contains("500", ex.Message);
        Assert.Contains("https://cryptify.postguard.eu/fileupload/init", ex.Message);
        Assert.Contains("boom", ex.Message);
    }

    [Fact]
    public void Ctor_AllowsEmptyBody()
    {
        var ex = new NetworkException(204, "", "https://pkg.postguard.eu/v2/parameters");

        Assert.Equal("", ex.Body);
        Assert.Equal("https://pkg.postguard.eu/v2/parameters", ex.Url);
    }
}
