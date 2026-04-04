using System.ComponentModel.DataAnnotations;

namespace SimpleWallet.Api.DTOs;

public class UpdateUserPatchRequest : IValidatableObject
{
    public string? Name { get; set; }
    public string? Email { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Name is null && Email is null)
        {
            yield return new ValidationResult(
                "At least one field must be provided.",
                new[] { nameof(Name), nameof(Email) }
            );
        }
    }
}
