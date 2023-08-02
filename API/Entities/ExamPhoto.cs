using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("ExamPhoto")]
    public class ExamPhoto
    {

        public int ExamPhotoId { get; set; }

        public string ExamPhotoUrl { get; set; }

        public string ExamPhotoPublicId { get; set; }

        public int AppExamsId { get; set; }

        public AppExams AppExams { get; set; }
    }
}