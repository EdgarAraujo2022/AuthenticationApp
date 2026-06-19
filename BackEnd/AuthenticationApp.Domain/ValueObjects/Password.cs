namespace AuthenticationApp.Domain.ValueObjects;

public class Password
{
    public string Hash { get; private set; } = default!;

    private Password() { }

    private Password(string hash)
    {
        Hash = hash;
    }

    public static Password FromHash(string hash)
    {
        return new Password(hash);
    }

    public static Password Create(string plainPassword, string hash)
    {
        if (string.IsNullOrWhiteSpace(plainPassword))
            throw new ArgumentException("Senha não pode ser vazia.");

        if (plainPassword.Length < 6)
            throw new ArgumentException("Senha deve ter pelo menos 6 caracteres.");

        return new Password(hash);
    }

    public override string ToString() => Hash;
}
