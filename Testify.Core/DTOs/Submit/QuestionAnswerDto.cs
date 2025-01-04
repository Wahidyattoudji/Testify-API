namespace TestifyWebAPI.Controllers
{
    public class QuestionAnswerDto
    {
        public int QuestionId { get; set; } // رقم السؤال
        public List<int> SelectedOptions { get; set; } = new List<int>(); // قائمة معرفات الخيارات المختارة
    }
}
