namespace E4A.PostGuard.Models;

public class RecipientBuilder
{
    public string Email { get; }
    internal string BaseType { get; }
    internal List<(string Type, string Value)> Extras { get; } = [];

    internal RecipientBuilder(string email, string baseType)
    {
        Email = email;
        BaseType = baseType;
    }

    public RecipientBuilder ExtraAttribute(string type, string value)
    {
        Extras.Add((type, value));
        return this;
    }
}
