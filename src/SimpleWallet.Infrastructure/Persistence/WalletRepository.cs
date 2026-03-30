using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleWallet.Application.Interfaces;
using SimpleWallet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SimpleWallet.Infrastructure.Persistence
{
	public class WalletRepository : IWalletRepository
	{
		private readonly WalletDbContext _context;

		public WalletRepository(WalletDbContext context)
		{
			_context = context;
		}

		public async Task<Wallet?> GetByIdAsync(Guid id)
		{
			return await _context.Wallets.Include(w => w.Transactions).FirstOrDefaultAsync(w => w.Id == id);
		}

		public async Task<IEnumerable<Wallet>> GetByUserIdAsync(Guid userId)
		{
			return await _context.Wallets.Where(w => w.UserId == userId).ToListAsync();
		}

		public async Task AddAsync(Wallet wallet)
		{
			await _context.Wallets.AddAsync(wallet);
		}

		public Task DeleteAsync(Wallet wallet)
		{
			_context.Wallets.Remove(wallet);
			return Task.CompletedTask;
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}

