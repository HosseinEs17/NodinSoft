using MediatR;
using NodinSoft.Domain.Authentication; // Corrected from NodinSoft.Entities.Authentication

namespace NodinSoft.Application.Authentication.Queries
{
    public class GetUserQuery : IRequest<User?> // Changed to User?
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}