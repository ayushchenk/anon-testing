using AnonTesting.BLL.Commands.Test;
using AnonTesting.BLL.Model;
using AnonTesting.BLL.Queries.Test;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnonTesting.API.Controllers
{
    [ApiController]
    [Route("api/tests/")]
    public class TestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("user/{id}")]
        public async Task<IActionResult> GetForUser(Guid id)
        {
            var userTests = await _mediator.Send(new GetUserTestsQuery(id));

            return Ok(userTests);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TestDto body)
        {
            Guid createdId = await _mediator.Send(new CreateTestCommand(body));

            return CreatedAtAction(nameof(Post), createdId);
        }
    }
}
