namespace SimpleWallet.Api.DTOs;

using System.ComponentModel.DataAnnotations;

public class TransferRequest : IValidatableObject
{
    public Guid FromWalletId { get; set; }
    public Guid ToWalletId { get; set; }

    [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
    public decimal Amount { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (FromWalletId == Guid.Empty)
            yield return new ValidationResult("FromWalletId is required.", new[] { nameof(FromWalletId) });

        if (ToWalletId == Guid.Empty)
            yield return new ValidationResult("ToWalletId is required.", new[] { nameof(ToWalletId) });

        if (FromWalletId != Guid.Empty && ToWalletId != Guid.Empty && FromWalletId == ToWalletId)
            yield return new ValidationResult("FromWalletId and ToWalletId must be different.", new[] { nameof(FromWalletId), nameof(ToWalletId) });
    }
}
