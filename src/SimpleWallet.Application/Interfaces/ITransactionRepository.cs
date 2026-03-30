using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleWallet.Domain.Entities;

namespace SimpleWallet.Application.Interfaces
{
	public interface ITransactionRepository
	{
		Task AddAsync(Transaction transaction);
		Task AddRangeAsync(IEnumerable<Transaction> transactions);
		Task SaveChangesAsync();
	}
}

