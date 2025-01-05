using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Testify.Core.DTOs;
using Testify.Infrastructure;
using TestifyWebAPI.DTOs;

namespace TestifyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Submittions : ControllerBase
    {
        public TestifyDbContext _dbContext { get; }
        public Submittions(TestifyDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllSubmissionsAsync()
        {
            var submissions = await _dbContext.Submissions
                .Include(s => s.Student)
                .Include(s => s.Test)
                .Include(s => s.SubmissionAnswers)
                    .ThenInclude(sa => sa.Question)
                .Include(s => s.SubmissionAnswers)
                    .ThenInclude(sa => sa.SelectedOption)
                .Include(s => s.Evaluations)
                .ToListAsync();

            if (!submissions.Any())
            {
                return NotFound("No submissions found.");
            }

            var submissionDtos = submissions.Select(s => new SubmissionDto
            {
                submittionId = s.SubmissionId,
                TestId = s.TestId,
                testName = s.Test.TestName,
                StudentId = s.StudentId,
                StudentName = s.Student.FullName,
                SubmittedAt = s.SubmittedAt,
                SubmissionAnswers = s.SubmissionAnswers.Select(sa => new SubmissionAnswerDto
                {
                    QuestionId = sa.QuestionId,
                    SelectedOptionId = sa.SelectedOptionId,
                }).ToList(),
                Evaluations = s.Evaluations.Select(ev => new EvaluationDto
                {
                    SubmissionId = ev.SubmissionId,
                    TotalScore = ev.TotalScore,
                    EvaluatedAt = ev.EvaluatedAt,
                    EvaluationId = ev.EvaluationId,
                    Feedback = ev.Feedback
                }).ToList(),
            });

            return Ok(submissionDtos);
        }



    }
}
