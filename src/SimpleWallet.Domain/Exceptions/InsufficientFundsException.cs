namespace SimpleWallet.Domain.Exceptions;
public class InsufficientFundsException : DomainException
{
    public InsufficientFundsException()
        : base("Insufficient funds.") { }
}
