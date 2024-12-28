using Microsoft.AspNetCore.Mvc;
using Testify.Core.Models;
using TestifyWebAPI.DTOs;
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

        [HttpPost]
        public async Task<IActionResult> CreateTestAsync([FromBody] TestDto request)
        {
            var Test = new Test()
            {
                TestName = request.TestName,
                CreatedBy = request.CreatedBy,
            };

            var newTest = await _testService.AddTest(Test);

            return Ok(newTest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTestAsync(int id, [FromBody] TestDto request)
        {
            var test = await _testService.GetById(id);

            test.TestName = request.TestName;
            test.CreatedBy = request.CreatedBy;


            var newTest = await _testService.UpdateTest(test);

            return Ok(newTest);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestAsynv(int id)
        {
            var deletedTest = await _testService.GetById(id);

            if (deletedTest == null)
            {
                return NotFound($"No Test found with ID : {id}");
            }

            await _testService.DeleteTest(deletedTest.TestId);

            return Ok(deletedTest);
        }
    }
}
