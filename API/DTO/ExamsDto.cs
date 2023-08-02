using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class ExamsDto
    {
        [Key]
        public int ExamId { get; set; }

        public string ExamName { get; set; }

        public string ExamDescription { get; set; }

        public string ExamPictureUrl { get; set; }

        public ExamPhotoDto ExamPicture { get; set;}

        public List<QuestionDto> ExamQuestion { get; set;}

        public int ExamGrade { get; set; }

        // Navigation property for many-to-many relationship with users
        public ICollection<UserExamDto> UserExams { get; set; }
    }
}