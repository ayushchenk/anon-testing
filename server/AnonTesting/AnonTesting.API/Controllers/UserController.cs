using AnonTesting.BLL.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnonTesting.API.Controllers
{
    [ApiController]
    [Route("api/user/")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var tokenResult = await _mediator.Send(command);

            if (tokenResult.IsSuccess)
            {
                return Ok(tokenResult.Value);
            }

            return BadRequest(tokenResult);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var tokenResult = await _mediator.Send(command);

            if (tokenResult.IsSuccess)
            {
                return Ok(tokenResult.Value);
            }

            return BadRequest(tokenResult);
        }
    }
}
