using AnonTesting.BLL.Commands.Test;
using AnonTesting.BLL.Model;
using AnonTesting.BLL.Queries.Test;
using Microsoft.AspNetCore.Mvc;
using MediatR;

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
        public async Task<IActionResult> Create([FromBody] TestDto body)
        {
            Guid createdId = await _mediator.Send(new CreateTestCommand(body));

            return CreatedAtAction(nameof(Create), createdId);
        }

        [HttpPost]
        [Route("complete")]
        public async Task<IActionResult> Complete([FromBody] CompleteTestCommand command)
        {
            var testResult = await _mediator.Send(command);

            return CreatedAtAction(nameof(Complete), testResult);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var testDto = await _mediator.Send(new GetTestByIdQuery(id));

            return Ok(testDto);
        }
    }
}
