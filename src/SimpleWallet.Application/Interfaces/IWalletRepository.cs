using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleWallet.Application.Interfaces
{
    public interface IWalletRepository
    {
        Task<SimpleWallet.Domain.Entities.Wallet?> GetByIdAsync(Guid id);
        Task<IEnumerable<SimpleWallet.Domain.Entities.Wallet>> GetByUserIdAsync(Guid userId);
        Task AddAsync(SimpleWallet.Domain.Entities.Wallet wallet);
        Task DeleteAsync(SimpleWallet.Domain.Entities.Wallet wallet);
        Task SaveChangesAsync();
    }
}
