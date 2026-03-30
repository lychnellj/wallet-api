
using SimpleWallet.Domain.Entities;
using SimpleWallet.Domain.Enums;
using SimpleWallet.Application.Interfaces;

namespace SimpleWallet.Application.Services;


public class WalletService : IWalletService
{
    private readonly IWalletRepository _walletRepository;
    private readonly ITransactionRepository _transactionRepository;

    public WalletService(IWalletRepository walletRepository, ITransactionRepository transactionRepository)
    {
        _walletRepository = walletRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task DepositAsync(Guid walletId, decimal amount)
    {
        var wallet = await _walletRepository.GetByIdAsync(walletId);
        if (wallet == null)
            throw new KeyNotFoundException("Wallet not found.");

        wallet.Deposit(amount);

        var transaction = new Transaction(wallet.Id, amount, TransactionType.Deposit);
        await _transactionRepository.AddAsync(transaction);
        await _walletRepository.SaveChangesAsync();
        await _transactionRepository.SaveChangesAsync();
    }

    public async Task WithdrawAsync(Guid walletId, decimal amount)
    {
        var wallet = await _walletRepository.GetByIdAsync(walletId);
        if (wallet == null)
            throw new KeyNotFoundException("Wallet not found.");

        wallet.Withdraw(amount);

        var transaction = new Transaction(wallet.Id, amount, TransactionType.Withdrawal);
        await _transactionRepository.AddAsync(transaction);
        await _walletRepository.SaveChangesAsync();
        await _transactionRepository.SaveChangesAsync();
    }

    public async Task TransferAsync(Guid fromWalletId, Guid toWalletId, decimal amount)
    {
        var fromWallet = await _walletRepository.GetByIdAsync(fromWalletId);
        var toWallet = await _walletRepository.GetByIdAsync(toWalletId);
        if (fromWallet == null || toWallet == null)
            throw new KeyNotFoundException("Wallet not found.");

        fromWallet.TransferTo(toWallet, amount);

        var withdrawal = new Transaction(fromWallet.Id, amount, TransactionType.Transfer);
        var deposit = new Transaction(toWallet.Id, amount, TransactionType.Deposit);

        await _transactionRepository.AddRangeAsync(new[] { withdrawal, deposit });
        await _walletRepository.SaveChangesAsync();
        await _transactionRepository.SaveChangesAsync();
    }

    public async Task<Wallet> CreateWalletAsync(Guid userId)
    {
        var wallet = new Wallet(userId);
        await _walletRepository.AddAsync(wallet);
        await _walletRepository.SaveChangesAsync();
        return wallet;
    }

    public async Task<Wallet?> GetWalletAsync(Guid walletId)
    {
        return await _walletRepository.GetByIdAsync(walletId);
    }

    public async Task<IEnumerable<Wallet>> GetWalletsByUserIdAsync(Guid userId)
    {
        return await _walletRepository.GetByUserIdAsync(userId);
    }

    public async Task DeleteWalletAsync(Guid walletId)
    {
        var wallet = await _walletRepository.GetByIdAsync(walletId);
        if (wallet == null)
            throw new KeyNotFoundException("Wallet not found.");

        await _walletRepository.DeleteAsync(wallet);
        await _walletRepository.SaveChangesAsync();
    }
}
