using MediatR;

namespace NodinSoft.Application.Authentication.Commands
{
    public class CreateUserCommand : IRequest<int>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }
}