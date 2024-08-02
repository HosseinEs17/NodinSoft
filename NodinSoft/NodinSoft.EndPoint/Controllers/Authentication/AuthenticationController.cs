using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NodinSoft.Application.Authentication.Commands;
using NodinSoft.Application.Authentication.Queries;
using NodinSoft.Entities.Authentication;
using NodinSoft.Persistence.Migrations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace NodinSoft.EndPoint.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
        {
            try
            {
                string? phoneRegex = _configuration.GetSection("PhoneRegex").Value;
                string? emailRegex = _configuration.GetSection("EmailRegex").Value;
                string? passwordRegex = _configuration.GetSection("PasswordRegex").Value;

                if (!Regex.IsMatch(command.PhoneNumber, phoneRegex))
                    return BadRequest("Invalid phone number format.");

                if (!Regex.IsMatch(command.Email, emailRegex))
                    return BadRequest("Invalid email format.");

                if (!Regex.IsMatch(command.Password, passwordRegex))
                    return BadRequest("Invalid password format. Password must have at least 8 characters, including at least one letter, one digit, and one special character");
            }
            catch (Exception ex)
            {

                throw;
            }

            int userId = await _mediator.Send(command);
            if (userId > 0)
                return Ok(new { UserId = userId });
            else
                return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] GetUserQuery query)
        {
            User user = await _mediator.Send(query);
            if (user is not null && user.Id is not 0)
            {
                var token = GenerateJwtToken(user);
                return Ok(new { token });
            }

            return Unauthorized();
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("phone_number", user.PhoneNumber)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(int.Parse(_configuration["Jwt:ExpireMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}