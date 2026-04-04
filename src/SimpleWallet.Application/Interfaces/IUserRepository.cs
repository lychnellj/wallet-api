using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleWallet.Domain.Entities;

namespace SimpleWallet.Application.Interfaces
{
    public interface IUserRepository
    {
		Task<User?> GetByIdAsync(Guid id);
		Task SaveChangesAsync();
    }
}