namespace Testify.Core.DTOs.Test.Show
{
    public class OptionDto
    {
        public int OptionId { get; set; } // معرّف الخيار
        public string OptionText { get; set; } = null!; // نص الخيار
        public bool IsCorrect { get; set; } // إذا كان الخيار صحيحًا
    }

}
