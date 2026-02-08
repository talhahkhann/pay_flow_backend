using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace PaymentSystem.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var result = await _mediator.Send(new GetCurrentUserQuery());

            if (result.IsFailure)
                return Unauthorized(result.Error);

            return Ok(result.Value);
        }
        [HttpGet("test")]
public IActionResult Test()
{
    return Ok("Users controller is working!");
}
    }
    
}
