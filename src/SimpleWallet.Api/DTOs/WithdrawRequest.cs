namespace SimpleWallet.Api.DTOs;

using System.ComponentModel.DataAnnotations;

public class WithdrawRequest : IValidatableObject
{
    public Guid WalletId { get; set; }

    [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
    public decimal Amount { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (WalletId == Guid.Empty)
            yield return new ValidationResult("WalletId is required.", new[] { nameof(WalletId) });
    }
}
