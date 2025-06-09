using MediatR;
using NodinSoft.Application.Authentication.Commands;
using NodinSoft.Domain.Authentication; // Corrected from NodinSoft.Entities.Authentication
using NodinSoft.Application.Authentication.Interfaces; // Corrected from NodinSoft.Interfaces.Authentication

namespace NodinSoft.Application.Authentication.CommandHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                throw new ArgumentNullException(nameof(request.Email), "Email cannot be null or empty for user creation.");
            if (string.IsNullOrWhiteSpace(request.Password)) // Assuming request.Password is the hash or value to be stored
                throw new ArgumentNullException(nameof(request.Password), "Password cannot be null or empty for user creation.");

            // PhoneNumber is nullable in command and User constructor
            User user = new User(request.Email, request.Password, request.PhoneNumber);

            await _userRepository.AddAsync(user);
            return user.Id;
        }
    }
}