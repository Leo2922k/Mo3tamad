using System.Runtime.InteropServices;

namespace API.Entities
{
    public class Question
    {

        public int QuestionId { get; set; }

        public string QuestionText { get; set; }

        public int QuestionMark { get; set; }

        public string QuestionPublicId { get; set; }

        public int AppExamsId { get; set; }

        public AppExams AppExams { get; set; }

        public List<Answers> QuestionAnswers { get; set; }

    }
}