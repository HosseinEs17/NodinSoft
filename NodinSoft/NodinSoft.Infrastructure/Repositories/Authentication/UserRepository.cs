using NodinSoft.Domain.Authentication; // Corrected
using NodinSoft.Application.Authentication.Interfaces; // Corrected
using Microsoft.EntityFrameworkCore;
using NodinSoft.Infrastructure.Persistence; // Corrected

namespace NodinSoft.Infrastructure.Repositories.Authentication // Corrected namespace
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

        public async Task<User?> GetAsync(string email, string passwordHash) // Changed parameters
        {
            return await _context.Users.Where(u => u.Email == email && u.Password == passwordHash)
                                       .FirstOrDefaultAsync();
        }
    }
}