using Microsoft.AspNetCore.Mvc;
using Testify.Core.Interfaces;
using Testify.Core.Models;

namespace TestifyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IGenericRepository<Test> _testRepository;

        public TestController(IGenericRepository<Test> repository)
        {
            _testRepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTest()
        {
            return Ok(await _testRepository.FindByIdAsync(1));
        }


    }
}
