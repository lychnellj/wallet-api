using Microsoft.EntityFrameworkCore;
using SimpleWallet.Application.Interfaces;
using SimpleWallet.Domain.Entities;

namespace SimpleWallet.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly WalletDbContext _context;

    public UserRepository(WalletDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
