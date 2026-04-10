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

    public NetworkException(int statusCode, string body)
        : base($"HTTP {statusCode}: {body}")
    {
        StatusCode = statusCode;
        Body = body;
    }
}

public class SealException : PostGuardException
{
    public SealException(string message) : base(message) { }
}
