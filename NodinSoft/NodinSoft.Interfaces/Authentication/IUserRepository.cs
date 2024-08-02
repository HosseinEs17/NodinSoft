using NodinSoft.Entities.Authentication;

namespace NodinSoft.Interfaces.Authentication
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
        Task<User> GetAsync(User user);
    }
}