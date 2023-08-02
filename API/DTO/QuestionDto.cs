namespace API.DTO
{
    public class QuestionDto
    {

        public int QuestionId { get; set; }

        public string QuestionText { get; set; }

        public int QuestionMark { get; set; }

        public List<AnswersDto> QuestionAnswers { get; set; }


    }
}