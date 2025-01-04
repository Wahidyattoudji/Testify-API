namespace Testify.Core.DTOs.Submit
{
    public class TestSubmissionDto
    {
        public int StudentId { get; set; } // رقم الطالب
        public List<QuestionAnswerDto> Answers { get; set; } = new List<QuestionAnswerDto>();
    }

    public class QuestionAnswerDto
    {
        public int QuestionId { get; set; } // رقم السؤال
        public List<int> SelectedOptions { get; set; } = new List<int>(); // قائمة معرفات الخيارات المختارة
    }

}