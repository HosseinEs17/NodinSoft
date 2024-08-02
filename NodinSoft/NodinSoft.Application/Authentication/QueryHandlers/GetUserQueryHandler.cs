using MediatR;
using NodinSoft.Application.Authentication.Queries;
using NodinSoft.Entities.Authentication;
using NodinSoft.Interfaces.Authentication;

namespace NodinSoft.Application.Authentication.QueryHandlers
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
    {
        private readonly IUserRepository _userRepository;
        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            User user = new User
            {
                Email = request.Email,
                Password = request.Password
            };
            return await _userRepository.GetAsync(user);
        }
    }
}