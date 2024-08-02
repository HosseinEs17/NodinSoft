using MediatR;
using NodinSoft.Entities.Authentication;

namespace NodinSoft.Application.Authentication.Queries
{
    public class GetUserQuery : IRequest<User>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}