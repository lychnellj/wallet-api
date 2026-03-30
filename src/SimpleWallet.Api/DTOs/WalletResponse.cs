namespace SimpleWallet.Api.DTOs;

public class WalletResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Balance { get; set; }
}
