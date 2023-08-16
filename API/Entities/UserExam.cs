using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    [PrimaryKey(nameof(Id), nameof(ExamId))]
    public class UserExam
    {
        public int Id { get; set; }

        public int ExamId { get; set; }

        public AppUser AppUser { get; set; }

        public AppExams AppExams { get; set; }

        public string UserExamScreenVideoUrl { get; set; }

        public string UserExamCamVideoUrl { get; set; }

        public int UserExamGrade { get; set; }
    }
}