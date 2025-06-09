using NodinSoft.Domain.Authentication; // Changed from NodinSoft.Entities.Authentication

namespace NodinSoft.Application.Authentication.Interfaces // Changed from NodinSoft.Interfaces.Authentication
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
        Task<User?> GetAsync(string email, string passwordHash); // Changed parameters
    }
}