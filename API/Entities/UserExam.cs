using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    [PrimaryKey(nameof(UserId), nameof(ExamId))]
    public class UserExam
    {
        public int UserId { get; set; }

        public int ExamId { get; set; }

        public AppUser AppUser { get; set; }

        public AppExams AppExams { get; set; }

        public string UserExamScreenVideoUrl { get; set; }

        public string UserExamCamVideoUrl { get; set; }

    }
}