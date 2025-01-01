namespace Testify.Core.DTOs.Test.Show
{
    public class QuestionDetailsDto
    {
        public int QuestionId { get; set; } // معرّف السؤال
        public string QuestionText { get; set; } = null!; // نص السؤال
        public string QuestionType { get; set; } = null!; // نوع السؤال (اختيار فردي أو متعدد)
        public ICollection<OptionDto> Options { get; set; } = new List<OptionDto>(); // قائمة الخيارات (للأسئلة ذات الخيارات)
    }

}
