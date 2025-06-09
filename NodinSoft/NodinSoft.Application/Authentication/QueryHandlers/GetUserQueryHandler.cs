using MediatR;
using NodinSoft.Application.Authentication.Queries;
using NodinSoft.Domain.Authentication; // Corrected from NodinSoft.Entities.Authentication
using NodinSoft.Application.Authentication.Interfaces; // Corrected from NodinSoft.Interfaces.Authentication

namespace NodinSoft.Application.Authentication.QueryHandlers
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User?> // Changed to User?
    {
        private readonly IUserRepository _userRepository;
        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User?> Handle(GetUserQuery request, CancellationToken cancellationToken) // Changed to User?
        {
            if (request.Email == null || request.Password == null)
            {
                // Or handle this more gracefully depending on requirements
                throw new ArgumentNullException("Email and Password must be provided for GetUserQuery.");
            }
            return await _userRepository.GetAsync(request.Email, request.Password);
        }
    }
}