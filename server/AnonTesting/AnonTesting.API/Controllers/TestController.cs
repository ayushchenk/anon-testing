using AnonTesting.BLL.Interfaces;
using AnonTesting.BLL.Model;
using Microsoft.AspNetCore.Mvc;

namespace AnonTesting.API.Controllers
{
    [Route("api/tests")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _service;

        public TestController(ITestService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var records = await _service.GetAllAsync();

            return Ok(records);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TestDto testDto)
        {
            var id = await _service.CreateAsync(testDto);

            return Ok(id);
        }
    }
}
