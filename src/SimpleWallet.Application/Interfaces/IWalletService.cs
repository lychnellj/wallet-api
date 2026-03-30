namespace SimpleWallet.Application.Interfaces;
using SimpleWallet.Domain.Entities;
public interface IWalletService
{
    Task DepositAsync(Guid walletId, decimal amount);
    Task WithdrawAsync(Guid walletId, decimal amount);
    Task TransferAsync(Guid fromWalletId, Guid toWalletId, decimal amount);

    Task<Wallet> CreateWalletAsync(Guid userId);
    Task<Wallet?> GetWalletAsync(Guid walletId);
    Task<IEnumerable<Wallet>> GetWalletsByUserIdAsync(Guid userId);
    Task DeleteWalletAsync(Guid walletId);
}
