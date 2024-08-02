using MediatR;
using NodinSoft.Application.Authentication.Commands;
using NodinSoft.Entities.Authentication;
using NodinSoft.Interfaces.Authentication;

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
            User user = new User
            {
                Email = request.Email,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber,
            };

            await _userRepository.AddAsync(user);
            return user.Id;
        }
    }
}