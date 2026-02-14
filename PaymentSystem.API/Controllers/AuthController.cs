using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using PaymentSystem.Application.Users.Commands;

namespace PaymentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        [EnableRateLimiting("OtpPolicy")]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsFailure)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("login")]
        [EnableRateLimiting("OtpPolicy")]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var token = await _mediator.Send(command);
            return Ok(new { Token = token });
        }
    }
}
