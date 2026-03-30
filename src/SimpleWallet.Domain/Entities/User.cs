namespace SimpleWallet.Domain.Entities;
using SimpleWallet.Domain.Exceptions;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public ICollection<Wallet> Wallets { get; private set; } = new List<Wallet>();

    public User(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainValidationException("Name is required");

        if (string.IsNullOrWhiteSpace(email))
            throw new DomainValidationException("Email is required");
        
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
    }
}
