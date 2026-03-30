using SimpleWallet.Domain.Exceptions;
using SimpleWallet.Domain.Validation;
namespace SimpleWallet.Domain.Entities;

public class Wallet
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public decimal Balance { get; private set; }

    public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

    public byte[]? RowVersion { get; private set;}

    private Wallet() { }

    public Wallet(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new DomainValidationException("UserId is required");

        Id = Guid.NewGuid();
        UserId = userId;
        Balance = 0;
    }

    public void Withdraw(decimal amount)
    {
        AmountValidator.ValidateAmount(amount);

        if (Balance < amount)
            throw new InsufficientFundsException(); // add exception
            
        Balance -= amount;
    }

    public void Deposit(decimal amount)
    {
        AmountValidator.ValidateAmount(amount);
        Balance += amount;
    }

    public void TransferTo(Wallet destinationWallet, decimal amount)
    {
        if (destinationWallet == null)
            throw new DomainValidationException("Destination wallet is required");

        if (destinationWallet.Id == Id)
            throw new DomainValidationException("Cannot transfer to the same wallet");

        AmountValidator.ValidateAmount(amount);

        Withdraw(amount);
        destinationWallet.Deposit(amount);
    }
}
