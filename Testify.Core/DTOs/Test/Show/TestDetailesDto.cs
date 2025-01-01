namespace Testify.Core.DTOs.Test.Show
{

    public class TestDetailesDto
    {
        public int TestId { get; set; } // معرّف الاختبار
        public string TestName { get; set; } = null!; // اسم الاختبار
        public int CreatedBy { get; set; } // معرّف المستخدم الذي أنشأ الاختبار
        public DateTime CreatedAt { get; set; } // تاريخ إنشاء الاختبار

        public ICollection<QuestionDetailsDto> Questions { get; set; } = new List<QuestionDetailsDto>(); // قائمة الأسئلة
    }

}
