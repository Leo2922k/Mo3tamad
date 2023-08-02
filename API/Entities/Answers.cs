namespace API.Entities
{
    public class Answers
    {
        public int AnswersId { get; set; }

        public string AnswersOption { get; set; }

        public bool AnswersTrue { get; set; }

        public string AnswersPublicId { get; set; }

        public int QuestionId { get; set; }
        
        public Question Question { get; set; }
    }
}