
using SimpleWallet.Domain.Exceptions;
namespace SimpleWallet.Domain.Validation;

public static class AmountValidator
{
    public static void ValidateAmount(decimal amount)
    {
        if (amount <= 0)
            throw new DomainValidationException("Amount must be positive.");
    }
}
