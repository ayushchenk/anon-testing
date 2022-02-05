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
            try
            {
                var userTests = await _mediator.Send(new GetUserTestsQuery(id));

                return Ok(userTests);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TestDto body)
        {
            try
            {
                Guid createdId = await _mediator.Send(new CreateTestCommand(body));

                return CreatedAtAction(nameof(Create), createdId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("complete")]
        public async Task<IActionResult> Complete([FromBody] CompleteTestCommand command)
        {
            try
            {
                var testResult = await _mediator.Send(command);

                return CreatedAtAction(nameof(Complete), testResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var testDto = await _mediator.Send(new GetTestByIdQuery(id));

                return Ok(testDto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
