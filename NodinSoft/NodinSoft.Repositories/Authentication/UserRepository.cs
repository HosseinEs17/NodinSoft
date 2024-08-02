using NodinSoft.Entities.Authentication;
using NodinSoft.Interfaces.Authentication;
using Microsoft.EntityFrameworkCore;
using NodinSoft.Persistance;

namespace NodinSoft.Repositories.Authentication
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;
        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetAsync(User user)
        {
            return await _context.Users.Where(u => u.Email == user.Email && u.Password == user.Password)
                                       .FirstOrDefaultAsync();
        }
    }
}