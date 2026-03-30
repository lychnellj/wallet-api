using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleWallet.Application.Interfaces;
using SimpleWallet.Domain.Entities;

namespace SimpleWallet.Infrastructure.Persistence
{
	public class TransactionRepository : ITransactionRepository
	{
		private readonly WalletDbContext _context;

		public TransactionRepository(WalletDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Transaction transaction)
		{
			await _context.Transactions.AddAsync(transaction);
		}

		public async Task AddRangeAsync(IEnumerable<Transaction> transactions)
		{
			await _context.Transactions.AddRangeAsync(transactions);
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}

