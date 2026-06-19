using System;
using System.Text.RegularExpressions;

namespace AuthenticationApp.Domain.ValueObjects;

public class Email
{
    public string Value { get; private set; } = default!;

    private Email() { }

    public Email(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email não pode ser vazio.");

        if (!IsValid(email))
            throw new ArgumentException("Email inválido.");

        Value = email;
    }

    private bool IsValid(string email)
    {
        var regex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
    }

    public override string ToString() => Value;
}