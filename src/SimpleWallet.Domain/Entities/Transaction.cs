using SimpleWallet.Domain.Enums;
using SimpleWallet.Domain.Exceptions;
using SimpleWallet.Domain.Validation;

namespace SimpleWallet.Domain.Entities;
using SimpleWallet.Domain.Exceptions;

public class Transaction
{
    public Guid Id { get; private set; }

    public Guid WalletId { get; private set; }
    public Wallet Wallet { get; private set; }

    public decimal Amount { get; private set; }

    public TransactionType Type { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Guid? DestinationWalletId { get; private set; }

    private Transaction() { }

    public Transaction(Guid walletId, decimal amount, TransactionType type, Guid? destinationWalletId = null)
    {
        if (walletId == Guid.Empty)
            throw new DomainValidationException("WalletId is required");

        AmountValidator.ValidateAmount(amount);

        Id = Guid.NewGuid();
        WalletId = walletId;
        Amount = amount;
        Type = type;
        CreatedAt = DateTime.UtcNow;
        DestinationWalletId = destinationWalletId;
    }
}
