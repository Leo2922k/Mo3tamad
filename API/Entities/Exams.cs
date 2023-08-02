using System.ComponentModel.DataAnnotations;
namespace API.Entities

{
    public class AppExams
    {
        [Key]
        public int ExamId { get; set; }

        public string ExamName { get; set; }

        public string ExamDescription { get; set; }

        public int ExamGrade { get; set; }

        public ExamPhoto ExamPicture { get; set;}

        public List<Question> ExamQuestions { get; set;}  

        public ICollection<UserExam> UserExams { get; set;}
    }
}