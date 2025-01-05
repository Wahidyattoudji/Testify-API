using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Testify.Infrastructure;
using TestifyWebAPI.DTOs;

namespace TestifyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluationController : ControllerBase
    {
        public TestifyDbContext _db { get; }

        public EvaluationController(TestifyDbContext dbContext)
        {
            _db = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllEvaluations()
        {
            var evaluations = await _db.Evaluations.ToListAsync();

            List<EvaluationDto> EvDto = new List<EvaluationDto>();

            foreach (var ev in evaluations)
            {
                EvDto.Add(new EvaluationDto
                {
                    EvaluationId = ev.EvaluationId,
                    SubmissionId = ev.SubmissionId,
                    TotalScore = ev.TotalScore,
                    Feedback = ev.Feedback,
                    EvaluatedAt = ev.EvaluatedAt,
                });
            }

            return Ok(EvDto);
        }


    }
}
