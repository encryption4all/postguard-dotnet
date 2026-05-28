namespace E4A.PostGuard.Exceptions;

public class PostGuardException : Exception
{
    public PostGuardException(string message) : base(message) { }
    public PostGuardException(string message, Exception inner) : base(message, inner) { }
}

public class NetworkException : PostGuardException
{
    public int StatusCode { get; }
    public string Body { get; }
    public string Url { get; }

    public NetworkException(int statusCode, string body, string url)
        : base($"HTTP {statusCode} at {url}: {body}")
    {
        StatusCode = statusCode;
        Body = body;
        Url = url;
    }
}

public class SealException : PostGuardException
{
    public SealException(string message) : base(message) { }
}
