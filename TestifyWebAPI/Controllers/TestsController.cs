using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Testify.Core.DTOs.Submit;
using Testify.Core.DTOs.Test.Create;
using Testify.Core.DTOs.Test.Show;
using Testify.Core.Models;
using Testify.Infrastructure;
using TestifyWebAPI.DTOs;
using TestifyWebAPI.Services.Contracts;

namespace TestifyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly ITestService _testService;
        private readonly IQuestionService questionService;
        private readonly IUserService _userService;
        private readonly ISubmissionService _submissionService;
        private readonly ISubmissionAnswerService _submissionAnswerService;
        private readonly TestifyDbContext _dbcontext;

        public TestsController(ITestService testService, IQuestionService questionService
            , IUserService userService, ISubmissionService submissionService
            , ISubmissionAnswerService submissionAnswerService, TestifyDbContext dbcontext)
        {
            _testService = testService;
            this.questionService = questionService;
            _userService = userService;
            _submissionService = submissionService;
            _submissionAnswerService = submissionAnswerService;
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public async Task<IActionResult> GetTests()
        {
            var tests = await _testService.GetAll();
            if (tests == null || !tests.Any())
            {
                return NotFound("No Test found.");
            }

            var testDto = new List<TestDetailesDto>();

            foreach (var test in tests)
            {
                testDto.Add(ToDto(test));
            }
            return Ok(testDto);
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
        public async Task<IActionResult> CreateTestAsync([FromBody] CreateTestDto request)
        {
            if (request == null
                || string.IsNullOrWhiteSpace(request.TestName)
                || request.Questions == null
                || !request.Questions.Any())
            {
                return BadRequest("Invalid test data. Ensure test name and questions are provided.");
            }


            // إنشاء قائمة الأسئلة مع الخيارات
            var questions = new List<Question>();

            foreach (var q in request.Questions)
            {
                var questionOptions = q.Options?.Select(o => new QuestionOption
                {
                    OptionText = o.OptionText,
                    IsCorrect = o.IsCorrect
                }).ToList();

                questions.Add(new Question
                {
                    QuestionText = q.QuestionText,
                    QuestionType = q.QuestionType,
                    QuestionOptions = questionOptions ?? new List<QuestionOption>()
                });
            }

            var test = new Test
            {
                TestName = request.TestName,
                CreatedBy = request.CreatedBy,
                CreatedAt = request.CreatedAt ?? DateTime.Now,
                Questions = questions // ربط الأسئلة بالاختبار
            };

            var newTest = await _testService.AddTest(test);

            if (newTest == null)
            {
                return StatusCode(500, "Error while creating the test.");
            }

            return Ok(ToDto(newTest));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTestAsync(int id, [FromBody] CreateTestDto request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.TestName) || request.Questions == null || !request.Questions.Any())
            {
                return BadRequest("Invalid test data. Ensure test name and questions are provided.");
            }

            // البحث عن الاختبار باستخدام ID
            var test = await _testService.GetById(id);

            if (test == null)
            {
                return NotFound("Test not found.");
            }

            // تحديث البيانات الأساسية للاختبار
            test.TestName = request.TestName;
            test.CreatedBy = request.CreatedBy;

            // الحصول على الأسئلة القديمة من قاعدة البيانات
            var existingQuestions = test.Questions.ToList();

            // قائمة جديدة للأسئلة التي سيتم إضافتها أو تعديلها
            var updatedQuestions = new List<Question>();

            foreach (var q in request.Questions)
            {
                var questionOptions = q.Options?.Select(o => new QuestionOption
                {
                    OptionText = o.OptionText,
                    IsCorrect = o.IsCorrect
                }).ToList();

                var existingQuestion = existingQuestions.FirstOrDefault(eq => eq.QuestionText == q.QuestionText);

                if (existingQuestion != null)
                {
                    // إذا كانت هناك أسئلة موجودة وتم تعديلها، نقوم بتحديثها
                    existingQuestion.QuestionText = q.QuestionText;
                    existingQuestion.QuestionType = q.QuestionType;
                    existingQuestion.QuestionOptions = questionOptions;  // تحديث الخيارات
                    updatedQuestions.Add(existingQuestion);
                    existingQuestions.Remove(existingQuestion);  // إزالة السؤال المعدل من الأسئلة القديمة
                }
                else
                {
                    // إذا كانت هذه سؤال جديد، نقوم بإضافته
                    var newQuestion = new Question
                    {
                        QuestionText = q.QuestionText,
                        QuestionType = q.QuestionType,
                        QuestionOptions = questionOptions
                    };
                    updatedQuestions.Add(newQuestion);
                }
            }

            // حذف الأسئلة التي لم تعد موجودة في الطلب (إذا كانت الأسئلة القديمة غير مستخدمة)
            foreach (var oldQuestion in existingQuestions)
            {
                await questionService.DeleteQuestion(oldQuestion.QuestionId); // حذف الأسئلة القديمة التي لم تعد موجودة
            }

            // إضافة الأسئلة المحدثة أو الجديدة إلى الاختبار
            test.Questions = updatedQuestions;

            // تحديث الاختبار في قاعدة البيانات
            var updatedTest = await _testService.UpdateTest(test);

            if (updatedTest == null)
            {
                return StatusCode(500, "Error while updating the test.");
            }

            return Ok(updatedTest);
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

        [HttpPost("{testId}/submit")]
        public async Task<IActionResult> SubmitTest(int testId, [FromBody] TestSubmissionDto request)
        {

            var submission = new Submission
            {
                TestId = testId,
                StudentId = request.StudentId,
                SubmittedAt = DateTime.Now
            };

            var newSubmission = await _submissionService.AddSubmission(submission);

            foreach (var answer in request.Answers)
            {
                foreach (var optionId in answer.SelectedOptions)
                {
                    var submissionAnswer = new SubmissionAnswer
                    {
                        SubmissionId = newSubmission.SubmissionId,
                        QuestionId = answer.QuestionId,
                        SelectedOptionId = optionId
                    };
                    await _submissionAnswerService.AddSubmissionAnswer(submissionAnswer);
                }
            }

            /// evaluation 
            var totalScore = await CalculateTotalScoreAsync(newSubmission.SubmissionId);

            var evaluation = new Evaluation
            {
                SubmissionId = newSubmission.SubmissionId,
                TotalScore = totalScore,
                Feedback = totalScore >= 50 ? "Passed" : "Failed",
                EvaluatedAt = DateTime.Now
            };


            await _dbcontext.Evaluations.AddAsync(evaluation);
            await _dbcontext.SaveChangesAsync();

            var EvDto = new EvaluationDto
            {
                EvaluationId = evaluation.EvaluationId,
                EvaluatedAt = evaluation.EvaluatedAt,
                TotalScore = evaluation.TotalScore,
                Feedback = evaluation.Feedback,
                SubmissionId = evaluation.SubmissionId
            };

            // return Ok(new
            /* {
                 message = "Submission recorded and evaluated successfully.",
                 submissionId = newSubmission.SubmissionId,
                 evaluation = new
                 {
                     totalScore = evaluation.TotalScore,
                     feedback = evaluation.Feedback
                 }
             });*/

            return Ok(EvDto);
        }

        [HttpGet("Submitions")]
        public async Task<IActionResult> Submitions()
        {
            var submitions = await _submissionService.GetAll();

            return Ok(submitions);
        }

        private async Task<int> CalculateTotalScoreAsync(int submissionId)
        {
            // Retrieve all submission answers with related data
            var submissionAnswers = await _dbcontext.SubmissionAnswers
                .Include(sa => sa.SelectedOption)
                .Include(sa => sa.Question)
                    .ThenInclude(q => q.QuestionOptions) // Include all options for each question
                .Where(sa => sa.SubmissionId == submissionId)
                .ToListAsync();

            if (!submissionAnswers.Any())
                throw new Exception("No answers found for this submission.");

            int totalQuestions = submissionAnswers.Select(sa => sa.QuestionId).Distinct().Count();
            int correctQuestionsCount = 0;

            // Group answers by question
            var answersGroupedByQuestion = submissionAnswers
                .GroupBy(sa => sa.QuestionId)
                .ToList();

            foreach (var group in answersGroupedByQuestion)
            {
                var question = group.First().Question;

                // Get all correct options for the question
                var correctOptions = question.QuestionOptions.Where(o => o.IsCorrect).Select(o => o.OptionId).ToList();

                // Get all selected options for the current question
                var selectedOptions = group.Select(sa => sa.SelectedOptionId).Where(id => id != null).Cast<int>().ToList();

                if (question.QuestionType == "Single Choice")
                {
                    // Single Choice: The answer is correct if only one option is selected and it's correct
                    if (selectedOptions.Count == 1 && correctOptions.Contains(selectedOptions.First()))
                    {
                        correctQuestionsCount++;
                    }
                }
                else if (question.QuestionType == "Multiple Choice")
                {
                    // Multiple Choice: The answer is correct if all correct options are selected and no extra options are selected
                    if (!selectedOptions.Except(correctOptions).Any() && !correctOptions.Except(selectedOptions).Any())
                    {
                        correctQuestionsCount++;
                    }
                }
            }

            // Calculate the total score
            int totalScore = (correctQuestionsCount * 100) / totalQuestions;

            return totalScore;
        }


        private TestDetailesDto ToDto(Test test)
        {
            var testDetailsDto = new TestDetailesDto
            {
                TestId = test.TestId,
                TestName = test.TestName,
                CreatedBy = test.CreatedBy,
                CreatedAt = (DateTime)test.CreatedAt,
                Questions = test.Questions.Select(q => new Testify.Core.DTOs.Test.Show.QuestionDetailsDto
                {
                    QuestionId = q.QuestionId,
                    QuestionText = q.QuestionText,
                    QuestionType = q.QuestionType,
                    Options = q.QuestionOptions.Select(o => new OptionDto
                    {
                        OptionId = o.OptionId,
                        OptionText = o.OptionText,
                        IsCorrect = o.IsCorrect
                    }).ToList()
                }).ToList()
            };
            return testDetailsDto;
        }
    }
}
