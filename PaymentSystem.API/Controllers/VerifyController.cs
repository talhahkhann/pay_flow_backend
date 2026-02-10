using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Application.Common.Models;

namespace PaymentSystem.API.Controllers
{
    [ApiController]
    [Route("api/verify")]
    public class VerifyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VerifyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Verify user's email using OTP code
        /// </summary>
        /// <param name="cmd">Contains Email and OTP Code</param>
        /// <returns>Result of verification</returns>
        [HttpPost("email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailCommand cmd)
        {
            if (cmd == null || string.IsNullOrWhiteSpace(cmd.Email) || string.IsNullOrWhiteSpace(cmd.Otp))
            {
                return BadRequest(Result.Failure("VerifyEmail.InvalidInput", "Email or OTP code is missing."));
            }

            // Send command to MediatR
            var result = await _mediator.Send(cmd);

            if (result.IsSuccess)
            {
                return Ok(result); // return success response
            }

            // return failure details
            return BadRequest(result);
        }
    }
}
