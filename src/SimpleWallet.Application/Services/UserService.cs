using SimpleWallet.Application.Interfaces;
using SimpleWallet.Domain.Entities;

namespace SimpleWallet.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> GetUserAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task UpdateProfileAsync(Guid id, string? name, string? email)
    {
        if (name is null && email is null)
            throw new ArgumentException("At least one field must be provided for update.");

        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new KeyNotFoundException("User not found.");

        user.UpdateProfile(name, email);
        await _userRepository.SaveChangesAsync();
    }
}
