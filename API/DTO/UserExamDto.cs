using Microsoft.EntityFrameworkCore;

namespace API.DTO
{
    public class UserExamDto
    {
        public string UserExamScreenVideoUrl { get; set; }

        public string UserExamCamVideoUrl { get; set; }

        public int UserExamGrade { get; set; }
    }
}