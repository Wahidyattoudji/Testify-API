using Microsoft.AspNetCore.Mvc;
using TestifyWebAPI.Services.Contracts;

namespace TestifyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestsController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTests()
        {
            var tests = await _testService.GetAll();
            if (tests == null || !tests.Any())
            {
                return NotFound("No Test found.");
            }
            return Ok(tests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTestById(int id)
        {
            var test = await _testService.GetById(id);
            if (test == null)
                return NotFound($"No Test With ID : {id}");

            return Ok(test);
        }


    }
}
