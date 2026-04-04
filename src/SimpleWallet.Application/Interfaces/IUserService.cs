using SimpleWallet.Domain.Entities;

namespace SimpleWallet.Application.Interfaces;

public interface IUserService
{
    Task<User?> GetUserAsync(Guid id);
    Task UpdateProfileAsync(Guid id, string? name, string? email);
}
