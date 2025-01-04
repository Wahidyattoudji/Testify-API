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
            // جلب جميع التقديمات مع البيانات المرتبطة بها
            var submissions = await _dbContext.Submissions
                .Include(s => s.Student)
                .Include(s => s.Test)
                .Include(s => s.SubmissionAnswers)
                    .ThenInclude(sa => sa.Question)
                .Include(s => s.SubmissionAnswers)
                    .ThenInclude(sa => sa.SelectedOption)
                .ToListAsync();

            // التحقق إذا لم تكن هناك أي تقديمات
            if (!submissions.Any())
            {
                return NotFound("No submissions found.");
            }

            // تحويل البيانات إلى DTO
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
                }).ToList()
            });

            // إعادة التقديمات في الرد
            return Ok(submissionDtos);
        }



    }
}
