namespace SimpleWallet.Domain.Entities;
using SimpleWallet.Domain.Exceptions;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public ICollection<Wallet> Wallets { get; private set; } = new List<Wallet>();

    private User() { } 

    public User(string name, string email)
    {
        Name = ValidateName(name);
        Email = ValidateEmail(email);
        Id = Guid.NewGuid();
    }

    public User(Guid id, string name, string email)
    {
        if (id == Guid.Empty)
            throw new DomainValidationException("User id is required");

        Id = id;
        Name = ValidateName(name);
        Email = ValidateEmail(email);
    }

    public void UpdateProfile(string? name, string? email)
    {
        if (name is not null)
            Name = ValidateName(name);

        if (email is not null)
            Email = ValidateEmail(email);
    }

    public static string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainValidationException("Name is required");

        return name.Trim();
    }

    public static string ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainValidationException("Email is required");

        var normalized = email.Trim();

        if(!normalized.Contains("@"))
            throw new DomainValidationException("Email format is invalid");

        return normalized;
    }
}
